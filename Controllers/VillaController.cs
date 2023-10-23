using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using primeraApi.Modelos.Datos;
using primeraApi.Modelos.Dto;


namespace primeraApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        public VillaController(ILogger<VillaController> logger)
        {

            _logger = logger;

        }

        [HttpGet]
        public IActionResult GetVillas()
        {
            _logger.LogInformation("Obtener todas las villas");
            return Ok(VillaStore.villaList);
        }
        [HttpGet("id:int", Name = "GetVilla")]
        public IActionResult GetVilla(int id)
        {
            var villa = VillaStore.villaList.FirstOrDefault(p => p.Id == id);
            if (villa == null)
            {
                _logger.LogError("Error con el id");
                return NotFound();
            };
            return Ok(villa);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult CrearVilla([FromBody] VillaDto nuevaVilla)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var verNombre = VillaStore.villaList.FirstOrDefault(p => p.Nombre.ToLower() == nuevaVilla.Nombre.ToLower());
            if (verNombre != null)
            {
                ModelState.AddModelError("NombreExiste", "La Villa con ese nombre ya existe");
                return BadRequest(ModelState);
            }
            if (nuevaVilla == null) return BadRequest();
            if (nuevaVilla.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            nuevaVilla.Id = VillaStore.villaList.OrderByDescending(p => p.Id).FirstOrDefault(nuevaVilla).Id + 1;

            VillaStore.villaList.Add(nuevaVilla);

            return CreatedAtRoute("GetVilla", new { id = nuevaVilla.Id }, nuevaVilla);
        }
        [HttpDelete]
        [Route("borrar/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            if (villa == null) return NotFound();

            VillaStore.villaList.Remove(villa);
            return NoContent();
        }
        [HttpPut]
        [Route("conput/{id}")]
        public IActionResult Editar(int id, [FromBody] VillaDto editarVilla)
        {
            if (id == 0 || editarVilla == null) return BadRequest();
            var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            if (villa == null) return NotFound();
            villa.Nombre = editarVilla.Nombre;
            villa.Ocupantes = editarVilla.Ocupantes;

            return NoContent();
        }
        [HttpPatch]
        [Route("contpatch/{id}")]
        public IActionResult EditarConPatch(int id, JsonPatchDocument<VillaDto> patchDto)
        {
            if(patchDto==null || id==0) return NoContent();
            var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            if (villa == null) return NotFound();
            patchDto.ApplyTo(villa, ModelState);
            if(!ModelState.IsValid) return BadRequest(ModelState);
            return NoContent();
        }
    }
   
}
