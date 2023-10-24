using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMapper _mapper;
        public VillaController(ILogger<VillaController> logger, AplicationDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetVillas()
        {
            var villas = await _db.Villas.ToListAsync();
            return Ok(villas);
        }
        [HttpGet("id:int", Name = "GetVilla")]
        public async Task<IActionResult> GetVilla(int id)
        {
            //var villa = VillaStore.villaList.FirstOrDefault(p => p.Id == id);
            var data = await _db.Villas.ToListAsync();
            var villa = data.FirstOrDefault(p => p.Id == id);
            if (villa == null)
            {
                _logger.LogError("Error con el id");
                return NotFound();
            };
            return Ok(villa);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CrearVilla([FromBody] VillaCreateDto nuevaVilla)
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
            var agregar = _mapper.Map<Villa>(nuevaVilla);
            _db.Villas.Add(agregar);
            await _db.SaveChangesAsync();
            return CreatedAtRoute("GetVilla", nuevaVilla);
        }
        [HttpDelete]
        [Route("borrar/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = _db.Villas.ToList().FirstOrDefault(v => v.Id == id);
            if (villa == null) return NotFound();

            _db.Villas.Remove(villa);
            await _db.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut]
        [Route("conput/{id}")]
        public async Task<IActionResult> Editar(int id, [FromBody] VillaUpdateDto editarVilla)
        {
            if (id == 0 || editarVilla == null) return BadRequest();
            //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            //if (villa == null) return NotFound();
            //villa.Nombre = editarVilla.Nombre;
            //villa.Ocupantes = editarVilla.Ocupantes;
            var villa = _db.Villas.ToList().FirstOrDefault(p => p.Id == id);
            if (villa == null) return NotFound();
            if (!ModelState.IsValid) return BadRequest();

            var guardar = _mapper.Map<Villa>(editarVilla);
            _db.Villas.Update(guardar);
            await _db.SaveChangesAsync();
            return NoContent();
        }
        [HttpPatch]
        [Route("contpatch/{id}")]
        public async Task<IActionResult> EditarConPatch(int id, JsonPatchDocument<VillaUpdateDto> patchDto)
        {
            if (patchDto == null || id == 0) return NoContent();
            //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            //if (villa == null) return NotFound();
            //patchDto.ApplyTo(villa, ModelState);
            var villa = _db.Villas.AsNoTracking().ToList().FirstOrDefault(p => p.Id == id);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (villa == null) return BadRequest();

            var convertir = _mapper.Map<VillaUpdateDto>(villa);
            patchDto.ApplyTo(convertir, ModelState);
            villa = _mapper.Map<Villa>(convertir);

            _logger.LogInformation("Aqui estamos bin");

            _db.Villas.Update(villa);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }

}
