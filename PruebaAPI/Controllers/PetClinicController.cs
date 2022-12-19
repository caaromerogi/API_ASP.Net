using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DB;
using Microsoft.AspNetCore.Mvc;

namespace PruebaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PetClinicController : ControllerBase
    {
       private readonly PetClinicContext _context;

       public PetClinicController(PetClinicContext context)
       {
        _context = context;
       }

       
    }
}