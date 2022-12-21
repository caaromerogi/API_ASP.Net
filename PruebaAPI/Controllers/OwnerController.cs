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
    public class OwnerController : ControllerBase
    {

        private readonly IOwnerService _service;

        public OwnerController(IOwnerService service)
        {
            _service = service;
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
        public async Task<IActionResult> AddOwner(CreateOwnerDTO owner){
            return Ok(await _service.AddOwner(owner));
        }

        [HttpPut("Put")]
        public async Task<IActionResult> UpdateOwner(int id, OwnerDTO owner){
            await _service.UpdateOwner(id, owner);
            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteOwner(int id){
            await _service.DeleteOwner(id);
            return Ok();
        }
    
    }
}