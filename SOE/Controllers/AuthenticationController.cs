using Microsoft.AspNetCore.Mvc;
using SOE.Api;
using SOE.Services;

namespace SOE.Controllers;

[ApiController]
[Route("api/authentication")]
public class AuthenticationController(
    AppDbContext appDbContext, 
    IAuthenticationService authenticationService
): ControllerBase {

    [HttpPost]
    public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request) {
        var voter = await appDbContext.Voters.FindAsync(request.VoterId);

        if (voter == default) {
            throw new ArgumentException($"Voter with ID {request.VoterId} not found.");
        }
        
        return await authenticationService.AuthenticateAsync(voter, request.Otp);
    }
    
}