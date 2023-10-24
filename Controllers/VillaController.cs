using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using primeraApi.Modelos;
using primeraApi.Modelos.Datos;
using primeraApi.Modelos.Dto;


namespace primeraApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        private readonly AplicationDbContext _db;
        public VillaController(ILogger<VillaController> logger, AplicationDbContext db)
        {

            _logger = logger;
            _db = db;
        }

        [HttpGet]
        public IActionResult GetVillas()
        {
            var villas = _db.Villas.ToList();
            return Ok(villas);
        }
        [HttpGet("id:int", Name = "GetVilla")]
        public IActionResult GetVilla(int id)
        {
            //var villa = VillaStore.villaList.FirstOrDefault(p => p.Id == id);
            var villa = _db.Villas.ToList().FirstOrDefault(p => p.Id == id);
            if (villa == null)
            {
                _logger.LogError("Error con el id");
                return NotFound();
            };
            return Ok(villa);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult CrearVilla([FromBody] Villa nuevaVilla)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (nuevaVilla == null) return BadRequest();
            var verNombre = _db.Villas.ToList().FirstOrDefault(p => p.Nombre.ToLower() == nuevaVilla.Nombre.ToLower());
            if (verNombre != null)
            {
                ModelState.AddModelError("NombreExiste", "La Villa con ese nombre ya existe");
                return BadRequest(ModelState);
            }

            if (nuevaVilla.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            _db.Villas.Add(nuevaVilla);
            _db.SaveChanges();
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
            var villa = _db.Villas.ToList().FirstOrDefault(v => v.Id == id);
            if (villa == null) return NotFound();

            _db.Villas.Remove(villa);
            _db.SaveChanges();
            return NoContent();
        }
        [HttpPut]
        [Route("conput/{id}")]
        public IActionResult Editar(int id, [FromBody] VillaDto editarVilla)
        {
            if (id == 0 || editarVilla == null) return BadRequest();
            //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            //if (villa == null) return NotFound();
            //villa.Nombre = editarVilla.Nombre;
            //villa.Ocupantes = editarVilla.Ocupantes;
            var villa = _db.Villas.ToList().FirstOrDefault(p => p.Id == id);
            if (villa == null) return NotFound();
            if (!ModelState.IsValid) return BadRequest();

            villa.Tarifa = editarVilla.Tarifa;
            villa.Nombre = editarVilla.Nombre;
            villa.MetrosCuadrados = editarVilla.MetrosCuadrados;
            villa.Amenidad = editarVilla.Amenidad;
            villa.Detalle = editarVilla.Detalle;
            villa.ActualizacionFecha = DateTime.Now;
            villa.ImagenUrl = editarVilla.ImagenUrl;
            villa.Ocupantes = editarVilla.Ocupantes;

            _db.SaveChanges();

            return NoContent();
        }
        [HttpPatch]
        [Route("contpatch/{id}")]
        public IActionResult EditarConPatch(int id, JsonPatchDocument<VillaDto> patchDto)
        {
            if (patchDto == null || id == 0) return NoContent();
            //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            //if (villa == null) return NotFound();
            //patchDto.ApplyTo(villa, ModelState);
            var villa = _db.Villas.ToList().FirstOrDefault(p => p.Id == id);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (villa == null) return BadRequest();
            VillaDto guardar = new()
            {
                Tarifa = villa.Tarifa,
                Nombre = villa.Nombre,
                MetrosCuadrados = villa.MetrosCuadrados,
                Amenidad = villa.Amenidad,
                Detalle = villa.Detalle,
                ImagenUrl = villa.ImagenUrl,
                Ocupantes = villa.Ocupantes
            };
            patchDto.ApplyTo(guardar, ModelState);

            villa.Tarifa = guardar.Tarifa;
            villa.Nombre = guardar.Nombre;
            villa.MetrosCuadrados = guardar.MetrosCuadrados;
            villa.Amenidad = guardar.Amenidad;
            villa.Detalle = guardar.Detalle;
            villa.ActualizacionFecha = DateTime.Now;
            villa.ImagenUrl = guardar.ImagenUrl;
            villa.Ocupantes = guardar.Ocupantes;

            _db.SaveChanges();
            return NoContent();
        }
    }

}
