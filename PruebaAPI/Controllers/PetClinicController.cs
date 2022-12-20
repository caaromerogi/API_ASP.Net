using AutoMapper;
using DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaAPI.DTO;
using PruebaAPI.Services.OwnerService;

namespace PruebaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PetClinicController : ControllerBase
    {

        private readonly IOwnerService _service;
        private readonly IMapper _mapper;

        public PetClinicController(IOwnerService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetAllOwners(){

            return Ok(await _service.GetOwners());
        }

        [HttpPost("Post")]
        public async Task<IActionResult> AddOwner(OwnerDTO owner){
            if(_service.AddOwner(owner).AsyncState != null){
                Console.WriteLine(_service.AddOwner(owner).AsyncState);
                
                return Ok(await _service.AddOwner(owner));
            }
            
            return BadRequest();
        }
    
    }
}