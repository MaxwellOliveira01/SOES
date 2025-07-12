using Microsoft.AspNetCore.Mvc;
using SOE.Api;
using SOE.Services;

namespace SOE.Controllers;

[ApiController]
[Route("api/submit-vote")]
public class SubmitVoteController(
    IVoterSessionService voterSessionService,
    ISubmitVoteService submitVoteService,
    AppDbContext appDbContext
): ControllerBase {

    [HttpPost]
    public async Task<IActionResult> SubmitVoteAsync(SubmitVoteRequest request) {

        var session = voterSessionService.GetSession(request.Session);

        if(!session.IsAuthenticated) {
            throw new UnauthorizedAccessException("You must be authenticated to submit a vote.");
        }

        var voter = await appDbContext.Voters.FindAsync(session.VoterId) ??
            throw new KeyNotFoundException("Voter not found.");


        await submitVoteService.SubmitAsync(voter, request.ElectionId, request.OptionId, request.PublicKeyPem, request.Signature);

        return Ok();

    }

}