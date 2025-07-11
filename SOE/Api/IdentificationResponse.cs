namespace SOE.Api;

public class IdentificationResponse {
    
    public string? ErrorMessage { get; set; }
    
    public bool Success { get; set; }
    
    public string Name { get; set; }
    
    public string Session { get; set; } = string.Empty;
    
}