using AutoMapper;
using DB;
using Microsoft.EntityFrameworkCore;
using PruebaAPI.DTO;

namespace PruebaAPI.Services.OwnerService;

public class OwnerService : IOwnerService
{
    private readonly PetClinicContext _context;
    private readonly IMapper _mapper;

    public OwnerService(PetClinicContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> AddOwner(OwnerDTO owner)
    {
        Owner ownerEntity = _mapper.Map<OwnerDTO, Owner>(owner);
        Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Owner> o = await _context.AddAsync(ownerEntity);     
        
        return await _context.SaveChangesAsync();
    }

    public async Task<List<OwnerDTO>> GetOwners()
    {
        List<Owner> ownersEntity = await _context.Owners.Include(o => o.Pets).ToListAsync();
        List<OwnerDTO> ownersDTO = _mapper.Map<List<Owner>, List<OwnerDTO>>(ownersEntity);
        return ownersDTO;
    }

    public Task UpdateOwner(int id, OwnerDTO owner)
    {
        throw new NotImplementedException();
    }

    
    public Task DeleteOwner(int id)
    {
        throw new NotImplementedException();
    }
}