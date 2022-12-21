using PruebaAPI.DTO;

namespace PruebaAPITests.MockData;

public class OwnerMockData
{
    public static List<OwnerDTO> GetOwnersData(){
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
                FirstName = "Carlos",
                LastName = "Romero",
                Pets = new List<PetDTO>{}
            }
        };
    }
}