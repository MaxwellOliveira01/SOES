using Microsoft.AspNetCore.DataProtection;
using SOE.Classes;
using SOE.Entities;

namespace SOE.Services;

public interface IVoterSessionService {

    string CreateSession(Voter voter, bool isAuthenticated = false);

    VoterSession GetSession(string session);

}

public class VoterSessionService(IDataProtectionProvider provider) : IVoterSessionService {
    
    private readonly IDataProtector _protector = provider.CreateProtector("VoterTokenService.Purpose");

    public string CreateSession(Voter voter, bool isAuthenticated = false) {
        var session = new VoterSession {
            VoterId = voter.Id, 
            IsAuthenticated = isAuthenticated
        };
        return Protect(session);
    }

    public VoterSession GetSession(string session) {
        if (string.IsNullOrEmpty(session)) {
            throw new ArgumentException("Token cannot be null or empty", nameof(session));
        }
        return Unprotect(session);
    }
    
    private string Protect(VoterSession session) {
        return _protector.Protect(System.Text.Json.JsonSerializer.Serialize(session));
    }
    
    private VoterSession Unprotect(string token) {
        var json = _protector.Unprotect(token);
        var voterSession = System.Text.Json.JsonSerializer.Deserialize<VoterSession>(json);
        if (voterSession == null) {
            throw new ArgumentException("Invalid token");
        }
        return voterSession;
    }
    
}