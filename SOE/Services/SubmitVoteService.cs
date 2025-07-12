using Microsoft.EntityFrameworkCore;
using SOE.Entities;

namespace SOE.Services;

public interface ISubmitVoteService {
    Task SubmitAsync(Voter voter, int electionId, int optionId, string publicKeyPem, string signature);
}

public class SubmitVoteService(
    AppDbContext appDbContext
) : ISubmitVoteService {
    
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

        var voterElection = new VoterElection {
            Voter = voter,
            VoterId = voter.Id,
            Election = election,
            ElectionId = electionId,
            Option = option,
            OptionId = optionId,
            VoteTime = DateTimeOffset.UtcNow,
            Signature = signature,
            VoterPublicKeyPem = publicKeyPem
        };
        
        try {
            appDbContext.VoterElections.Add(voterElection);
            await appDbContext.SaveChangesAsync();
        } catch (DbUpdateException _){
            throw new InvalidOperationException("Already voted");
        }
        
    }
    
}