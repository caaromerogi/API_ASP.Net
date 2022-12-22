using AutoMapper;
using DB;
using Microsoft.EntityFrameworkCore;
using PruebaAPI.DTO;
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

    public async Task<ICollection<PetDTO>> GetPets()
    {
        List<Pet> pets = await _context.Pets.ToListAsync();
        List<PetDTO> petsDTO = _mapper.Map<List<Pet>, List<PetDTO>>(pets);
        return petsDTO;
    }

    public async Task CreatePet(CreatePetDTO petDTO)
    {
        Pet pet = _mapper.Map<CreatePetDTO, Pet>(petDTO);
        await _context.Pets.AddAsync(pet);
        await _context.SaveChangesAsync();
    }

    //TODO: Validate that the owner exists.
     public async Task EditPet(int id, PetDTO petDTO)
    {
        var pet = await _context.Pets.FindAsync(id);

        if(pet is null){
            throw new ElementNotFoundException($"The pet with ID {id} wasn't found");
        }

        if(id != petDTO.PetID){
            throw new InconsistentDataException("The Id of request doesn't match with Pet Id");
        }
        pet.Name = petDTO.Name;
        pet.Type = petDTO.Type;
        pet.HospitalizeDate = petDTO.HospitalizeDate;
        pet.DischargeDate = petDTO.DischargeDate;
        pet.OwnerId = petDTO.OwnerId;
        await _context.SaveChangesAsync();
        
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

   
    public async Task<PetDTO> GetPetById(int id)
    {
        var pet = await _context.Pets.FindAsync(id);

        if(pet is null){
            throw new ElementNotFoundException($"The pet with ID {id} wasn't found");
        }

        return _mapper.Map<Pet,PetDTO>(pet);
    }

}