using PruebaAPI.DTO;

namespace PruebaAPI.Services.OwnerService;

public interface IOwnerService
{
    public abstract Task<List<OwnerDTO>> GetOwners();
    public abstract Task<OwnerDTO> GetOwnerById(int id);
    public abstract Task<CreateOwnerDTO> AddOwner(CreateOwnerDTO owner);
    public abstract Task UpdateOwner(int id, OwnerDTO owner);
    public abstract Task DeleteOwner(int id);
}     