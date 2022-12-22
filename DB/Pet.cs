using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DB;

public class Pet
{ 
    public int PetID { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public DateTime HospitalizeDate { get; set; }
    public DateTime DischargeDate { get; set; }
    public int? OwnerId { get; set; }
    public virtual Owner? Owner {get;set;}        
}