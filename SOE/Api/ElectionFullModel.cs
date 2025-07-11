namespace SOE.Api;

public class ElectionFullModel {
    
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public List<OptionModel> Options { get; set; }
    
}