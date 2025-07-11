using Microsoft.EntityFrameworkCore;
using SOE.Entities;

namespace SOE.Services;

public interface ISubmitVoteService {
    
}

public class SubmitVoteService(AppDbContext appDbContext) : ISubmitVoteService {
    
    public async Task SubmitVoteAsync(Voter voter, Guid electionId, Guid optionId) {

        var election = await appDbContext.Elections.FindAsync(electionId);
        
        if (election == null) {
            throw new Exception("Election not found");
        }
        
        var option = await appDbContext.Options.FindAsync(optionId);
        
        if (option == null) {
            throw new Exception("Option not found");
        }
        
        if(option.ElectionId != electionId) {
            throw new Exception("Option does not belong to the specified election");
        }
        
        if(appDbContext.VoterElections.Any(ve => ve.VoterId == voter.Id && ve.ElectionId == electionId)) {
            throw new Exception("Voter has already voted in this election");
        }

        var voterElection = new VoterElection {
            Voter = voter,
            VoterId = voter.Id,
            Election = election,
            ElectionId = electionId,
            Option = option,
            OptionId = optionId,
            VoteTime = DateTimeOffset.UtcNow,
            // add other fields of signature here
        };
        
        try {
            appDbContext.VoterElections.Add(voterElection);
            await appDbContext.SaveChangesAsync();
        } catch (DbUpdateException exception) {
            throw new Exception("Already voted");
        }
        
    }
    
}