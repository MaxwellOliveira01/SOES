namespace SOE.Api;

public class ElectionVoterModel {
    
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public bool HasVoted { get; set; }
    
}