namespace SOE.Api;

public class SubmitVoteRequest {
    
    public Guid VoterId { get; set; }
    
    public Guid ElectionId { get; set; }
    
    public Guid OptionId { get; set; }
    
    public string Token { get; set; }

    // put other fields of signature here
    
}