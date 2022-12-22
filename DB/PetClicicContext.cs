using Microsoft.EntityFrameworkCore;

namespace DB;
public class PetClinicContext : DbContext
{
    public PetClinicContext(DbContextOptions<PetClinicContext> options)
    :base(options)
    {
        
    }

    public DbSet<Owner> Owners{get;set;}
    public DbSet<Pet> Pets {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Owner>().ToTable("Owner");
        modelBuilder.Entity<Pet>().ToTable("Pet");

        modelBuilder.Entity<Owner>().HasKey(o => o.OwnerId);
        modelBuilder.Entity<Pet>().HasKey(p => p.PetID);

        modelBuilder.Entity<Pet>().HasOne(p => p.Owner);
        modelBuilder.Entity<Owner>().HasMany(o => o.Pets);

        modelBuilder.Entity<Owner>().Property(p => p.FirstName).IsRequired();
    
    }
}
