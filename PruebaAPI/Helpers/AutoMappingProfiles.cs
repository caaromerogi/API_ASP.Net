using AutoMapper;
using DB;
using PruebaAPI.DTO;

namespace PruebaAPI.Helpers;

public class AutoMappingProfiles : Profile
{
    public AutoMappingProfiles()
    {
        CreateMap<OwnerDTO, Owner>().ReverseMap();
        CreateMap<CreateOwnerDTO, Owner>().ReverseMap();
        CreateMap<PetDTO, Pet>().ReverseMap();
        CreateMap<CreatePetDTO, Pet>().ReverseMap();
    }
}