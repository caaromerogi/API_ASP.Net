using AutoMapper;
using DB;
using Microsoft.EntityFrameworkCore;
using PruebaAPI.DTO;
using PruebaAPI.Exceptions;

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

    public async Task<List<OwnerDTO>> GetOwners()
    {
        List<Owner> ownersEntity = await _context.Owners.Include(o => o.Pets).ToListAsync();
        List<OwnerDTO> ownersDTO = _mapper.Map<List<Owner>, List<OwnerDTO>>(ownersEntity);
        return ownersDTO;
    }

      public async Task<OwnerDTO> GetOwnerById(int id)
    {
        var owner = await _context.Owners.FindAsync(id);
        
        if(owner is not null){
            var ownerDTO = _mapper.Map<Owner, OwnerDTO>(owner);
            return ownerDTO;
        }

        throw new ElementNotFoundException($"Cannot find the owner with ID:{id}");
    }

    public async Task UpdateOwner(int id, OwnerDTO owner)
    {
        var ownerEntity = await _context.Owners.FindAsync(id);
        
        if(ownerEntity is null){
            throw new ElementNotFoundException($"Cannot find the owner with ID:{id}");
        }

        ownerEntity.FirstName = owner.FirstName;
        ownerEntity.LastName = owner.LastName;
        ownerEntity.Pets = _mapper.Map<ICollection<PetDTO>, ICollection<Pet>>(owner.Pets);
        await _context.SaveChangesAsync();

    }

    public async Task<int> AddOwner(OwnerDTO owner)
    {
        Owner ownerEntity = _mapper.Map<OwnerDTO, Owner>(owner);
        Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Owner> o = await _context.AddAsync(ownerEntity);     
        
        return await _context.SaveChangesAsync();
    }
    
    public async Task DeleteOwner(int id)
    {
        var owner = await _context.Owners.FindAsync(id);

        if(owner is null){
            throw new ElementNotFoundException($"Cannot find the owner with ID:{id}");
        }
        _context.Owners.Remove(owner);
        await _context.SaveChangesAsync();
    }


}