using Microsoft.EntityFrameworkCore;
using SOE.Entities;
using System.Security.Cryptography;

namespace SOE.Services;

public interface IVoteService {
    Task SubmitAsync(Voter voter, int electionId, int optionId, string publicKeyPem, string signature);
}

public class VoteService(
    AppDbContext appDbContext,
    ServerService serverService
) : IVoteService {
    
    public async Task SubmitAsync(Voter voter, int electionId, int optionId, string publicKeyPem, string signature) {

        var election = await appDbContext.Elections.FindAsync(electionId)
            ?? throw new InvalidOperationException("Election not found");

        var option = await appDbContext.Options.FindAsync(optionId)
            ?? throw new InvalidOperationException("Option not found");
        
        if(option.ElectionId != electionId) {
            throw new InvalidOperationException("Option does not belong to the specified election");
        }
        
        if(await appDbContext.VoterElections.CountAsync(ve => ve.VoterId == voter.Id && ve.ElectionId == electionId) > 0) {
            throw new InvalidOperationException("Voter has already voted in this election");
        }

        if(!SignatureService.Verify(publicKeyPem, optionId, signature)) {
            throw new InvalidOperationException("Invalid signature for the vote option");
        }

        using RSA rsa = RSA.Create();
        rsa.ImportFromPem(publicKeyPem);

        byte[] signatureBytes = Convert.FromBase64String(signature);
        byte[] publicKeyBytes = rsa.ExportSubjectPublicKeyInfo();

        var voterElection = new VoterElection {
            Voter = voter,
            VoterId = voter.Id,
            Election = election,
            ElectionId = electionId,
            Option = option,
            OptionId = optionId,
            VoteTime = DateTimeOffset.UtcNow,
            Signature = signatureBytes,
            VoterPublicKey = publicKeyBytes,
            ServerId = serverService.Server.Id,
            ServerSignature = serverService.Sign(signatureBytes),
        };
        
        try {
            appDbContext.VoterElections.Add(voterElection);
            await appDbContext.SaveChangesAsync();
        } catch (DbUpdateException _){
            throw new InvalidOperationException("Already voted");
        }

    }

    public async Task<bool> IsVoteValid(VoterElection voterElection) {
        
        if(!SignatureService.Verify(voterElection.VoterPublicKey, voterElection.OptionId, voterElection.Signature)) {
            return false;
        }

        var server = await appDbContext.Servers.FirstAsync(s => s.Id == voterElection.ServerId);
        var rsa = RSA.Create();
        rsa.ImportSubjectPublicKeyInfo(server.PublicKey, out _);

        return rsa.VerifyData(voterElection.Signature, voterElection.ServerSignature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
    }

}