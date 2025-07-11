using Microsoft.AspNetCore.Mvc;
using SOE.Api;
using SOE.Services;

namespace SOE.Controllers;

[ApiController]
[Route("api/authentication")]
public class AuthenticationController(
    IAuthenticationService authenticationService,
    IVoterSessionService voterSessionService,
    AppDbContext appDbContext
): ControllerBase {

    [HttpPost]
    public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request) {
        var session = voterSessionService.GetSession(request.Session);
        var voter = await appDbContext.Voters.FindAsync(session.VoterId);

        if (voter == null) { // should not happen, since we protect this with dataProtection
            throw new ArgumentException($"Voter with ID {session.VoterId} not found.");
        }
        
        return await authenticationService.AuthenticateAsync(voter, request.Otp);
    }
    
}