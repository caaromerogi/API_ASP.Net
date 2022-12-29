using DB;

namespace PruebaAPITests.MockData;

public class OwnerBuilder
{
    public Owner Build(){
        return new Owner{
                OwnerId = 12,
                FirstName = "Henry",
                LastName = "Romero",
                Pets = new List<Pet>{
                    new Pet{
                        PetID = 1,
                        Name = "Azul",
                        Type = "Dog",
                        HospitalizeDate = new DateTime(2022,10,20),
                        DischargeDate = new DateTime(2022,10,23)
                    }
                }
            };
    }

    public List<Owner> BuildList(){
        return new List<Owner>{
            new Owner{
                OwnerId = 12,
                FirstName = "Henry",
                LastName = "Romero",
                Pets = new List<Pet>{
                    new Pet{
                        PetID = 1,
                        Name = "Azul",
                        Type = "Dog",
                        HospitalizeDate = new DateTime(2022,10,20),
                        DischargeDate = new DateTime(2022,10,23)
                    }
                }
            },
            new Owner{
                OwnerId = 1,
                FirstName = "Carlos",
                LastName = "Romero",
                Pets = new List<Pet>{}
            }
        };
    }

    public  List<Owner> BuildEmptyList(){
        return new List<Owner>();
    }

    public Owner BuildUpdate(){
        return new Owner{
                OwnerId = 12,
                FirstName = "Jose",
                LastName = "Gil",
                Pets = new List<Pet>{
                    new Pet{
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