using GoodDeeds.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoodDeeds.Infrastructure.DbContexts;

public class GoodDeedsDbContext : DbContext
{
    public GoodDeedsDbContext(DbContextOptions<GoodDeedsDbContext> options)
        : base(options)
    {
    }
    
    public virtual DbSet<FamilyMember> FamilyMembers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FamilyMember>().ToTable("FamilyMembers", "test");
        
        base.OnModelCreating(modelBuilder);
    }
}