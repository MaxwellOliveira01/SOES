using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOE.Entities;

public class Server {

    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public DateTime StartsUtc { get; set; }

    [NotMapped]
    public DateTimeOffset Starts {
        get => new DateTimeOffset(StartsUtc, TimeSpan.Zero);
        set => StartsUtc = value.UtcDateTime;
    }

    public byte[] PublicKey { get; set; }

}
