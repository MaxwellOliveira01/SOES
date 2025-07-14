using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SOE.Api;
using SOE.Services;

namespace SOE.Controllers;

[ApiController]
[Route("api/elections")]
public class ElectionsController(
    AppDbContext appDbContext, 
    IVoterSessionService voterSessionService, 
    IVoteService voteService
) : ControllerBase {

    [HttpPost]
    public async Task<ElectionFullModel> GetElectionAsync(VoterElectionModelRequest request) {

        var session = voterSessionService.GetSession(request.Session);

        var election = await appDbContext.Elections
            .Include(e => e.Options)
            .FirstOrDefaultAsync(e => e.Id == request.ElectionId);

        if (election == null) {
            throw new ArgumentException($"Election with ID {request.ElectionId} not found.");
        }

        var voterElection = await appDbContext.VoterElections
            .FirstOrDefaultAsync(ve => ve.ElectionId == request.ElectionId 
                && ve.VoterId == session.VoterId);

        return new(){
            Id = election.Id,
            Name = election.Name,
            HasVoted = voterElection != null,
            Description = election.Description,
            Options = [..election.Options.Select(o => new OptionModel {
                Id = o.Id,
                Name = o.Name,
            })],
        };

    }

    [HttpGet("result/{id}")]
    public async Task<ElectionResult> GetResultAsync(int id){
        return await voteService.GetElectionResultAsync(id);
    }

}

