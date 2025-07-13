namespace SOE.Api;

public class ElectionResult{

    public int Id { get; set; }
    
    public string Name { get; set; }

    public int AnnuledVotes { get; set; }

    public List<OptionResult> Options { get; set; }

}
