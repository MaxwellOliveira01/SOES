using Microsoft.EntityFrameworkCore;
using SOE.Api;

namespace SOE.Services;

public interface IIdentificationService {
    Task<IdentificationResponse> IdentifyAsync(string givenEmail, bool sendOtp);
}

public class IdentificationService(
    AppDbContext appDbContext, 
    IVoterSessionService voterSessionService, 
    IOtpService otpService,
    IEmailSender emailSender
) : IIdentificationService {
    
    public async Task<IdentificationResponse> IdentifyAsync(string givenEmail, bool sendOtp) {

        var normalizedEmail = givenEmail.Trim().ToLower();
        
        var voter = await appDbContext.Voters.
            FirstOrDefaultAsync(voter => voter.Email.ToLower() == normalizedEmail);

        if (voter == default) {
            return new IdentificationResponse {
                Success = false,
                ErrorMessage = "User not found, check given email and try again."
            };
        }

        if(sendOtp) {
            var otp = await otpService.CreateAsync(voter);
            await emailSender.SendOtpAsync(voter.Email, otp);
        }

        return new IdentificationResponse {
            Success = true,
            ErrorMessage = null,
            Name = voter.Name,
            Session = voterSessionService.CreateSession(voter)
        };
        
    }
    
}