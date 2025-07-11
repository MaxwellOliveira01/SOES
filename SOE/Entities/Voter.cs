using System.ComponentModel.DataAnnotations;

namespace SOE.Entities;

public class Voter {

    [Key]
    public Guid Id { get; set; }
    
    [MaxLength(200)]
    public string Name { get; set; }
    
    [MaxLength(200)]
    public string Email { get; set; }
    
    public List<Otp> Otps { get; set; } = [];
    
    public List<VoterElection> Elections { get; set; } = [];
    
}