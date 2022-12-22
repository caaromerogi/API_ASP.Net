namespace PruebaAPI.DTO;

public class CreatePetDTO
{
    public string Name { get; set; }
    public string Type { get; set; }
    public DateTime HospitalizeDate { get; set; }
    public DateTime DischargeDate { get; set; }
    public int? OwnerId { get; set; }  
}