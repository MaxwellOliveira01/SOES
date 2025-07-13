using Microsoft.EntityFrameworkCore;

using SOE.Entities;

namespace SOE.Services;

public class AppDbContext: DbContext {
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
        
    }

    public DbSet<Voter> Voters { get; set; }
    
    public DbSet<Election> Elections { get; set; }
    
    public DbSet<Option> Options { get; set; }
    
    public DbSet<VoterElection> VoterElections { get; set; }

    public DbSet<Otp> Otps { get; set; }

    public DbSet<Server> Servers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {

        modelBuilder.Entity<VoterElection>()
            .HasIndex(ve => new { ve.VoterId, ve.ElectionId }).IsUnique();

        modelBuilder.Entity<VoterElection>()
            .HasIndex(ve => new { ve.Signature }).IsUnique();

        modelBuilder.Entity<VoterElection>()
            .HasIndex(ve => new { ve.ServerSignature }).IsUnique();

        base.OnModelCreating(modelBuilder);
    }
    
}