using PruebaAPI.DTO;

namespace PruebaAPI.Services.PetService;

public interface IPetService
{
    public Task<ICollection<PetDTO>> GetPets();
    public Task<PetDTO> GetPetById(int id);
    public Task CreatePet(CreatePetDTO petDTO);
    public Task EditPet(int id, PetDTO petDTO);
    public Task DeletePet(int id);
}