namespace SOE.Classes;

public class VoterSession {
    
    public Guid VoterId { get; set; }
    
    public bool IsAuthenticated { get; set; }
    
    // TODO: add expiration date
    
}