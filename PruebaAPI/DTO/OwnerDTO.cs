namespace PruebaAPI.DTO;

public class OwnerDTO 
{
    public int OwnerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public virtual ICollection<PetDTO> Pets { get; set; }
}