namespace SOE.Api;

public class AuthenticationRequest {
    public string Session { get; set; } = string.Empty;
    public string Otp { get; set; } = string.Empty;

}