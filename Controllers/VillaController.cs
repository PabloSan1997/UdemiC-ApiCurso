using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using primeraApi.Modelos.Datos;
using primeraApi.Modelos.Dto;


namespace primeraApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetVillas()
        {
            return Ok(VillaStore.villaList);
        }
        [HttpGet("id:int",Name ="GetVilla")]
        public IActionResult GetVilla(int id)
        {
            var villa = VillaStore.villaList.FirstOrDefault(p => p.Id == id);
            if (villa == null) return NotFound();
            return Ok(villa);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult CrearVilla([FromBody] VillaDto nuevaVilla) 
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (nuevaVilla == null) return BadRequest();
            if (nuevaVilla.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            nuevaVilla.Id = VillaStore.villaList.OrderByDescending(p => p.Id).FirstOrDefault(nuevaVilla).Id+1;

            VillaStore.villaList.Add(nuevaVilla);

            return CreatedAtRoute("GetVilla", new {id=nuevaVilla.Id}, nuevaVilla);
        }
    }
}
