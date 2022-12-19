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
        modelBuilder.Entity<Owner>().ToTable("Owner");
        modelBuilder.Entity<Pet>().ToTable("Pet");
    }
}
