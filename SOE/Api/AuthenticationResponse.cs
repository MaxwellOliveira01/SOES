namespace SOE.Api;

public class AuthenticationResponse {
    
    public bool Success { get; set; }
    
    public string? ErrorMessage { get; set; } = string.Empty;
    public string Session { get; set; } = string.Empty;

    public List<ElectionVoterModel> Elections { get; set; } = [];

}