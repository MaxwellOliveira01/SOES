using Microsoft.EntityFrameworkCore;
using SOE.Api;
using SOE.Entities;
using System.Security.Cryptography;

namespace SOE.Services;

public interface IVoteService {
    
    Task SubmitAsync(Voter voter, int electionId, int optionId, string publicKeyPem, string signature);

    Task<ElectionResult> GetElectionResultAsync(int electionId);

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
            VoterId = voter.Id,
            ElectionId = electionId,
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
        } catch (DbUpdateException ex) when(ex.InnerException is Microsoft.Data.Sqlite.SqliteException sqliteEx && sqliteEx.SqliteErrorCode == 19) { // unique constraint
            throw new InvalidOperationException("Already voted");
        }

    }

    public async Task<ElectionResult> GetElectionResultAsync(int electionId) {

        var election = await appDbContext.Elections.FirstAsync(e => e.Id == electionId);

        var voterElections = await appDbContext.VoterElections
            .Where(ve => ve.ElectionId == electionId)
            .Include(ve => ve.Option)
            .Include(ve => ve.Server)
            .ToListAsync();

        var optionVoteCount = new Dictionary<int, int>();
        int annuledVoteCount = 0;

        foreach (var voterElection in voterElections) {
            if(IsVoteValid(voterElection, voterElection.Server)) {

                if (!optionVoteCount.ContainsKey(voterElection.OptionId)) {
                    optionVoteCount.Add(voterElection.OptionId, 0);
                }
                
                optionVoteCount[voterElection.OptionId]++;

            } else {
                annuledVoteCount++;
            }
        }
        
        var allOptions = await appDbContext.Options.Where(o => o.ElectionId == electionId).ToListAsync();

        var optionResult = allOptions.Select(o => {
            return new OptionResult() {
                Id = o.Id,
                Name = o.Name,
                Votes = optionVoteCount.GetValueOrDefault(o.Id)
            };
        });


        return new ElectionResult() {
            Id = electionId,
            Name = election.Name,
            AnnuledVotes = annuledVoteCount,
            Options = optionResult.OrderByDescending(r => r.Votes).ToList(),
        };

    }


    private static bool IsVoteValid(VoterElection voterElection, Server server) {
        
        if(!SignatureService.Verify(voterElection.VoterPublicKey, voterElection.OptionId, voterElection.Signature)) {
            return false;
        }

        var rsa = RSA.Create();
        rsa.ImportSubjectPublicKeyInfo(server.PublicKey, out _);

        return rsa.VerifyData(voterElection.Signature, voterElection.ServerSignature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
    }

}