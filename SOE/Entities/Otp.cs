using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOE.Entities;

public class Otp {
    
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [MaxLength(20)] 
    public string Value { get; set; }  // TODO: store encrypted value
    
    public DateTime ExpirationUtc { get; set; }

    [NotMapped]
    public DateTimeOffset Expiration {
        get => new DateTimeOffset(ExpirationUtc, TimeSpan.Zero);
        set => ExpirationUtc = value.UtcDateTime;
    }
    
    public int VoterId { get; set; }

    public Voter Voter { get; set; }
    
    [ConcurrencyCheck]
    public bool Used { get; set; }
    
}