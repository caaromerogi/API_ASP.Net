using Microsoft.AspNetCore.Mvc;
using PruebaAPI.DTO;
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

        [HttpGet("Get")]
        public async Task<ActionResult> GetAllPets(){
            return Ok(await _service.GetPets());
        }

        [HttpGet("Get/{id}")]
        public async Task<ActionResult> GetPetById(int id){
            return Ok(await _service.GetPetById(id));
        }

        [HttpPost("Post")]
        public async Task<ActionResult> CreatePet(CreatePetDTO petDTO){
            await _service.CreatePet(petDTO);
            return Ok();
        }

        [HttpPut("Put/{id}")]
        public async Task<ActionResult> EditPet(int id, PetDTO petDTO){
            await _service.EditPet(id, petDTO);
            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeletePet(int id){
            await _service.DeletePet(id);    
            return Ok();
        }


    }
