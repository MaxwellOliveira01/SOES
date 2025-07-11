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

    public DbSet<Token> Tokens { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
     
        modelBuilder.Entity<Entities.VoterElection>()
            .HasKey(ve => new { ve.VoterId, ve.ElectionId });
        
        base.OnModelCreating(modelBuilder);
    }
    
}