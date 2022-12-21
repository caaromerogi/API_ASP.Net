using AutoMapper;
using DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaAPI.DTO;
using PruebaAPI.Filters;
using PruebaAPI.Services.OwnerService;

namespace PruebaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [TypeFilter(typeof(ExceptionManagerFilter))]
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

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetOwnerById(int id){
            return Ok(await _service.GetOwnerById(id));
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