using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using SOE.Api;

namespace SOE.Services;

public interface IIdentificationService {
    Task<IdentificationResponse> IdentifyAsync(string givenEmail);
}

public class IdentificationService: IIdentificationService {
    private readonly AppDbContext appDbContext;

    public IdentificationService(AppDbContext appDbContext) {
        this.appDbContext = appDbContext;
    }
    
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
            Token = voter.Id.ToString() // TODO: use dataProtection
        };
        
    }
    
}