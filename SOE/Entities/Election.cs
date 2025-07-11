using System.ComponentModel.DataAnnotations;

namespace SOE.Entities;

public class Election {
    
    [Key]
    public Guid Id { get; set; }
    
    [MaxLength(200)]
    public string Name { get; set; }
    
    public List<Option> Options { get; set; }
    
    public List<VoterElection> Voters { get; set; } = [];
    
}