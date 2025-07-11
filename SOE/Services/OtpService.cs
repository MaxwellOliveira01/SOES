using Microsoft.EntityFrameworkCore;
using SOE.Entities;

namespace SOE.Services;

public interface IOtpService {

    Task<string> CreateAsync(Guid voterId);

    Task<bool> ValidateAsync(Guid voterId, string token);
    
}

public class OtpService(AppDbContext appDbContext) : IOtpService {
    
    public async Task<string> CreateAsync(Guid voterId) {

        var token = new Otp {
            Id = Guid.NewGuid(), // TODO: CombProvider?
            VoterId = voterId,
            // TODO: mover esse tempo e o tamanho do token para um OtpConfig ou TokenConfig
            Expiration = DateTime.UtcNow.AddMinutes(2),
            Value = GenerateTokenValue(6)
        };

        await appDbContext.Otps.AddAsync(token);
        await appDbContext.SaveChangesAsync();
        
        return token.Value;
    }
    
    public async Task<bool> ValidateAsync(Guid voterId, string token) {
        
        var tokenEntity = await appDbContext.Otps
            .Where(o => o.VoterId == voterId && o.Value == token)
            .FirstOrDefaultAsync();

        if (tokenEntity == null) {
            return false;
        }

        if (tokenEntity.Expiration < DateTimeOffset.Now) {
            return false;
        }

        if (tokenEntity.Used) {
            return false;
        }

        tokenEntity.Used = true;
        await appDbContext.SaveChangesAsync();

        return true;
    }

    private static string GenerateTokenValue(int length) {
        var random = new Random();
        var result = string.Empty;
        
        for(var i = 0; i < length; i++) {
            int minDigit = (i > 0 ? 0 : 1); // ensure not leading zeros
            result += random.Next(minDigit, 10).ToString();
        }

        return result;

    }
    
}