namespace SOE.Api;

public class AuthenticationRequest {
    
    public Guid VoterId { get; set; }
    public string Otp { get; set; }

}