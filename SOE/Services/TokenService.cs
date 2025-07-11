using Microsoft.EntityFrameworkCore;
using SOE.Entities;

namespace SOE.Services;

public interface ITokenService {

    Task<string> CreateToken(Guid voterId);

    Task<bool> ValidateTokenAsync(Guid voterId, string token);
    
}

public class TokenService(AppDbContext appDbContext) : ITokenService {
    
    public async Task<string> CreateToken(Guid voterId) {

        var token = new Token {
            Id = Guid.NewGuid(), // TODO: CombProvider?
            VoterId = voterId,
            // TODO: mover esse tempo e o tamanho do token para um OtpConfig ou TokenConfig
            Expiration = DateTime.UtcNow.AddMinutes(2),
            Value = GenerateTokenValue(6)
        };

        await appDbContext.Tokens.AddAsync(token);
        await appDbContext.SaveChangesAsync();
        
        return token.Value;
    }
    
    public async Task<bool> ValidateTokenAsync(Guid voterId, string token) {
        
        var tokenEntity = await appDbContext.Tokens
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