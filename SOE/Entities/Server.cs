// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;
//
// namespace SOE.Entities;
//
// public class Server {
//     
//     [Key]
//     public Guid Id { get; set; }
//     
//     public DateTime StartedDateUtc { get; set; }
//
//     [NotMapped]
//     public DateTimeOffset StartedDate {
//         get => new DateTimeOffset(StartedDateUtc, TimeSpan.Zero);
//         set => StartedDateUtc = value.UtcDateTime;
//     }
//     
//     public byte[] PublicKey { get; set; }
//     
// }