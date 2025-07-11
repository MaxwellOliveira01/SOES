namespace SOE.Api;

public class AuthenticationResponse {
    
    public bool Success { get; set; }
    
    public string ErrorMessage { get; set; }

    public List<ElectionVoterModel> Elections { get; set; } = [];

}