using Microsoft.EntityFrameworkCore;

namespace DB;
public class PetClinicContext : DbContext, IPetClinicContext
{
    public PetClinicContext(DbContextOptions<PetClinicContext> options)
    :base(options)
    {
        
    }

    public PetClinicContext()
    {
    }

    public virtual DbSet<Owner> Owners{get;set;}
    public virtual DbSet<Pet> Pets {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Owner>().ToTable("Owner");
        modelBuilder.Entity<Pet>().ToTable("Pet");

        modelBuilder.Entity<Owner>().HasKey(o => o.OwnerId);
        modelBuilder.Entity<Pet>().HasKey(p => p.PetID);

        modelBuilder.Entity<Pet>()
        .HasOne(p => p.Owner)
        .WithMany(o => o.Pets)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Owner>().Property(p => p.FirstName).IsRequired();
    }
}
