using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB;

public class Pet
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PetID { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public DateTime HospitalizeDate { get; set; }
    public DateTime DischargeDate { get; set; }
    public int OwnerId { get; set; }
    [ForeignKey("OwnerId")]
    public virtual Owner Owner {get;set;}        
}