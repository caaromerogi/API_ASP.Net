using PruebaAPI.DTO;

namespace PruebaAPI.Services.OwnerService;

public interface IOwnerService
{
    public abstract Task<List<OwnerDTO>> GetOwners();
    public abstract Task<int> AddOwner(OwnerDTO owner);
    public abstract Task UpdateOwner(int id, OwnerDTO owner);
    public abstract Task DeleteOwner(int id);
}