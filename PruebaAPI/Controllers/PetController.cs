using Microsoft.AspNetCore.Mvc;
using PruebaAPI.Filters;
using PruebaAPI.Services.PetService;

namespace PruebaAPI.Controllers;

    [ApiController]
    [Route("[controller]")]
    [TypeFilter(typeof(ExceptionManagerFilter))]
    public class PetController : ControllerBase
    {
        private readonly IPetService _service;

        public PetController(IPetService service)
        {
            _service = service;
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeletePet(int id){
            await _service.DeletePet(id);    
            return Ok();
        }

    }
