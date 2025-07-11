using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using SOE.Api;

namespace SOE.Services;

public interface IIdentificationService {
    Task<IdentificationResponse> IdentifyAsync(string givenEmail);
}

public class IdentificationService(AppDbContext appDbContext, IVoterSessionService voterSessionService) : IIdentificationService {
    
    public async Task<IdentificationResponse> IdentifyAsync(string givenEmail) {

        var normalizedEmail = givenEmail.Trim().ToLower();
        
        var voter = await appDbContext.Voters.
            FirstOrDefaultAsync(voter => voter.Email.ToLower() == normalizedEmail);

        if (voter == default) {
            return new IdentificationResponse {
                Success = false,
                ErrorMessage = "User not found, check your email and try again."
            };
        }

        return new IdentificationResponse {
            Success = true,
            ErrorMessage = null,
            Name = voter.Name,
            Session = voterSessionService.CreateSession(voter)
        };
        
    }
    
}