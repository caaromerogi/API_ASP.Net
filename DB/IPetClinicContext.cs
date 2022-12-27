using Microsoft.EntityFrameworkCore;

namespace DB;

public interface IPetClinicContext
{
    DbSet<Owner> Owners {get; set;}
    DbSet<Pet> Pets {get; set;}
}