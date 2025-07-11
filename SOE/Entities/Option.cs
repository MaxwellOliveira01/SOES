using System.ComponentModel.DataAnnotations;

namespace SOE.Entities;

public class Option {
    
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; }

    public Guid ElectionId { get; set; }
    public Election Election { get; set; }
    
}