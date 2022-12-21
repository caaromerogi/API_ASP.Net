namespace PruebaAPI.DTO;

public class CreateOwnerDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public virtual ICollection<CreatePetDTO> Pets { get; set; }
}

