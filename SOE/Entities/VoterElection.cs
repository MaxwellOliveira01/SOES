using System.ComponentModel.DataAnnotations.Schema;

namespace SOE.Entities;

public class VoterElection {
    
    public Guid VoterId { get; set; }
    public Voter Voter { get; set; }

    public Guid ElectionId { get; set; }
    public Election Election { get; set; }

    public byte[] VoterPublicKey { get; set; }
    
    public byte[] VoterSignature { get; set; }
    
    public Guid OptionId { get; set; }
    public Option Option { get; set; }
    
    public DateTime VoteTimeUtc { get; set; }

    [NotMapped]
    public DateTimeOffset VoteTime {
        get => new DateTimeOffset(VoteTimeUtc, TimeSpan.Zero);
        set => VoteTimeUtc = value.UtcDateTime;
    }
    
}