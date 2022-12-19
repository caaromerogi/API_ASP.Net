namespace PruebaAPI.DTO;

public class PetDTO
{
    public int PetID { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public DateTime HospitalizeDate { get; set; }
    public DateTime DischargeDate { get; set; }
    public int OwnerId { get; set; }

}