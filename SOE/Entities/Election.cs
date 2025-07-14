using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOE.Entities;

public class Election {
    
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [MaxLength(200)]
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public List<Option> Options { get; set; }
    
    public List<VoterElection> Voters { get; set; } = [];
    
}