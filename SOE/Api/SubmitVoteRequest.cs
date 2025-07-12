namespace SOE.Api;

public class SubmitVoteRequest {
    
    public int VoterId { get; set; }
    
    public int ElectionId { get; set; }
    
    public int OptionId { get; set; }
    
    public string Token { get; set; }

    // put other fields of signature here
    
}