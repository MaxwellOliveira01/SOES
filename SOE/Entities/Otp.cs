using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOE.Entities;

public class Otp {

    [Key] public Guid Id { get; set; } = Guid.Empty;

    [MaxLength(20)] public string Value { get; set; } = string.Empty;  // TODO: store encrypted value
    
    public DateTime ExpirationUtc { get; set; }

    [NotMapped]
    public DateTimeOffset Expiration {
        get => new DateTimeOffset(ExpirationUtc, TimeSpan.Zero);
        set => ExpirationUtc = value.UtcDateTime;
    }
    
    public Guid VoterId { get; set; } = Guid.Empty;

    public Voter Voter { get; set; } = new Voter();
    
    [ConcurrencyCheck]
    public bool Used { get; set; } = false;
    
}