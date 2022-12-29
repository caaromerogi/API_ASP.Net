using PruebaAPI.DTO;

namespace PruebaAPITests.MockData;
//Convertir a un builder
public class OwnerBuilderDTO
{
    public OwnerDTO Build(){
        return new OwnerDTO{
                OwnerId = 12,
                FirstName = "Henry",
                LastName = "Romero",
                Pets = new List<PetDTO>{
                    new PetDTO{
                        PetID = 1,
                        Name = "Azul",
                        Type = "Dog",
                        HospitalizeDate = new DateTime(2022,10,20),
                        DischargeDate = new DateTime(2022,10,23)
                    }
                }
            };
    }

    public List<OwnerDTO> BuildList(){
        return new List<OwnerDTO>{
            new OwnerDTO{
                OwnerId = 12,
                FirstName = "Henry",
                LastName = "Romero",
                Pets = new List<PetDTO>{
                    new PetDTO{
                        PetID = 1,
                        Name = "Azul",
                        Type = "Dog",
                        HospitalizeDate = new DateTime(2022,10,20),
                        DischargeDate = new DateTime(2022,10,23)
                    }
                }
            },
            new OwnerDTO{
                OwnerId = 1,
                FirstName = "Carlos",
                LastName = "Romero",
                Pets = new List<PetDTO>{}
            }
        };
    }

    public  List<OwnerDTO> BuildEmptyList(){
        return new List<OwnerDTO>();
    }

    public OwnerDTO BuildUpdate(){
        return new OwnerDTO{
                OwnerId = 12,
                FirstName = "Jose",
                LastName = "Gil",
                Pets = new List<PetDTO>{
                    new PetDTO{
                        PetID = 1,
                        Name = "Azul",
                        Type = "Dog",
                        HospitalizeDate = new DateTime(2022,10,20),
                        DischargeDate = new DateTime(2022,10,23)
                    }
                }
            }; 
    }

}