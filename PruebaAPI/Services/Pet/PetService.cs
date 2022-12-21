using AutoMapper;
using DB;
using PruebaAPI.Exceptions;

namespace PruebaAPI.Services.PetService;

public class PetService : IPetService
{
    private readonly PetClinicContext _context;
    private readonly IMapper _mapper;

    public PetService(PetClinicContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task DeletePet(int id)
    {
        var pet = await _context.Pets.FindAsync(id);

        if(pet is null){
            throw new ElementNotFoundException($"Pet with Id {id} doesn't exist");
        }
        _context.Pets.Remove(pet);
        await _context.SaveChangesAsync();
    }
}