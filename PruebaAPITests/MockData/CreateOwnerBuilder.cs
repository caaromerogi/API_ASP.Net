using PruebaAPI.DTO;

namespace PruebaAPITests.MockData;

public class CreateOwnerDTOBuilder
{
    public CreateOwnerDTO Build(){
        return new CreateOwnerDTO{
                FirstName = "Henry",
                LastName = "Romero",
                Pets = new List<CreatePetDTO>{
                    new CreatePetDTO{
                        Name = "Azul",
                        Type = "Dog",
                        HospitalizeDate = new DateTime(2022,10,20),
                        DischargeDate = new DateTime(2022,10,23)
                    }
                }
            };
    }

    public List<CreateOwnerDTO> BuildList(){
        return new List<CreateOwnerDTO>{
            new CreateOwnerDTO{
                FirstName = "Henry",
                LastName = "Romero",
                Pets = new List<CreatePetDTO>{
                    new CreatePetDTO{
                        Name = "Azul",
                        Type = "Dog",
                        HospitalizeDate = new DateTime(2022,10,20),
                        DischargeDate = new DateTime(2022,10,23)
                    }
                }
            },
            new CreateOwnerDTO{
                FirstName = "Carlos",
                LastName = "Romero",
                Pets = new List<CreatePetDTO>{}
            }
        };
    }

    public  List<CreateOwnerDTO> BuildEmptyList(){
        return new List<CreateOwnerDTO>();
    }

}