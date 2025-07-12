namespace SOE.Api;

public class SubmitVoteRequest {
    
    public string Session { get; set; }
    
    public int ElectionId { get; set; }
    
    public int OptionId { get; set; }
    
    public string PublicKeyPem { get; set; }

    public string Signature { get; set; }
    
}