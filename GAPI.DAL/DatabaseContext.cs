using Microsoft.EntityFrameworkCore;
using GAPI.DAL.Models;

namespace GAPI.DAL;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> config) : base(config)
    {
        Database.EnsureCreated();
    }


    public required DbSet<UserDb> Users { get; init; }

    public required DbSet<SessionDb> Sessions { get; init; }

    public required DbSet<MessageDb> Messages { get; init; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserDb>(s =>
        {
            s.HasKey(s => s.Email);
        });

        modelBuilder.Entity<SessionDb>(s =>
        {
            s.HasKey(s => s.Id);
            s.HasOne<UserDb>().WithMany().HasForeignKey(s => s.UserEmail);
        });

        modelBuilder.Entity<MessageDb>(s =>
        {
            s.HasKey(s => new { s.Index, s.SessionId });
            s.HasOne<SessionDb>().WithMany().HasForeignKey(s => s.SessionId);
        });

        base.OnModelCreating(modelBuilder);
    }
}
