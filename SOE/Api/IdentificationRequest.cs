namespace SOE.Api;

public class IdentificationRequest {
    public string Email { get; set; } = string.Empty;

    public bool SendOtp { get; set; } = false;

}