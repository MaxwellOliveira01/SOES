using Microsoft.AspNetCore.Mvc;
using SOE.Api;
using SOE.Services;

namespace SOE.Controllers;

[ApiController]
[Route("api/otp")]
public class OtpController(
    IVoterSessionService voterSessionService,
    IOtpService otpService,
    IEmailSender emailSender,
    AppDbContext appDbContext
): ControllerBase {

    [HttpPost("send")]
    public async Task<IActionResult> SendOtpAsync(SendOtpRequest request) {
        var session = voterSessionService.GetSession(request.Session);
        var voter = appDbContext.Voters.FirstOrDefault(v => v.Id == session.VoterId);

        if (voter == null) { // should not happen, since we protect this with dataProtection
            throw new ArgumentException($"Voter with id {session.VoterId} not found");
        }

        var otp = await otpService.CreateAsync(voter);
        await emailSender.SendOtpAsync(voter.Email, otp);

        return Ok();
    }

}