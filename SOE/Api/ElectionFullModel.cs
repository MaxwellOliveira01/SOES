namespace SOE.Api;

public class ElectionFullModel {
    
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public List<OptionModel> Options { get; set; }
    
}