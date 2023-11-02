using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using primeraApi.Modelos;
using primeraApi.Modelos.Dto;
using primeraApi.Repositorio.IRepositorio;
using System.Net;

namespace primeraApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        private readonly IVillaRepositorio _villaRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public VillaController(ILogger<VillaController> logger, IVillaRepositorio villaRepo, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _villaRepo = villaRepo;
            _response = new();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetVillas()
        {
            var villas = await _villaRepo.ObtenerTodo();
            _response.Resultado = villas;
            _response.statusCode = HttpStatusCode.OK;
            return Ok(_response);
        }
        [HttpGet("id:int", Name = "GetVilla")]
        [Authorize]
        public async Task<ActionResult<APIResponse>> GetVilla(int id)
        {
            try
            {
                //var villa = VillaStore.villaList.FirstOrDefault(p => p.Id == id);
                var villa = await _villaRepo.Obtener(p => p.Id == id);
                if (villa == null)
                {
                    _logger.LogError("Error con el id");
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                };
                _response.Resultado = villa;
                _response.statusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception e)
            {
                _response.IsExitoso = false;
                _response.ErrorMessage = new List<string>() { e.ToString() };
            }
            return _response;
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<APIResponse>> CrearVilla([FromBody] VillaCreateDto nuevaVilla)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.Resultado = ModelState;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                if (nuevaVilla == null) return BadRequest();
                var verNombre = await _villaRepo.Obtener(p => p.Nombre.ToLower() == nuevaVilla.Nombre.ToLower());

                if (verNombre != null)
                {
                    ModelState.AddModelError("NombreExiste", "La Villa con ese nombre ya existe");
                    _response.Resultado = ModelState;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var agregar = _mapper.Map<Villa>(nuevaVilla);
                agregar.FechaCreacion = DateTime.Now;
                agregar.ActualizacionFecha = DateTime.Now;
                await _villaRepo.Crear(agregar);
                _response.Resultado = CreatedAtRoute("GetVilla", nuevaVilla);
                _response.statusCode = HttpStatusCode.Created;
                return Ok(_response);
            }
            catch (Exception e)
            {
                _response.IsExitoso = false;
                _response.ErrorMessage = new List<string>() { e.ToString() };
            }
            return _response;
        }
        [HttpDelete]
        [Route("borrar/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> Delete(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var villa = await _villaRepo.Obtener(p => p.Id == id);
                if (villa == null)
                {
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.Resultado = "No se encontro elemento con ese id";
                    return BadRequest(_response);
                }

                var elemento = _mapper.Map<Villa>(villa);
                await _villaRepo.Eliminar(elemento);

                return NoContent();
            }
            catch (Exception e)
            {
                _response.IsExitoso = false;
                _response.ErrorMessage = new List<string>() { e.ToString() };
            }
            return _response;
        }
        [HttpPut]
        [Route("conput/{id}")]
        public async Task<ActionResult<APIResponse>> Editar(int id, [FromBody] VillaUpdateDto editarVilla)
        {
            try
            {
                if (id == 0 || editarVilla == null) return BadRequest();
                //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
                //if (villa == null) return NotFound();
                //villa.Nombre = editarVilla.Nombre;
                //villa.Ocupantes = editarVilla.Ocupantes;
                var villa = await _villaRepo.Obtener(p => p.Id == id, tracked: false);
                if (villa == null) return NotFound();
                if (!ModelState.IsValid) return BadRequest();

                var guardar = _mapper.Map<Villa>(editarVilla);
                await _villaRepo.Actualizar(guardar);

                return NoContent();
            }
            catch (Exception e)
            {
                _response.IsExitoso = false;
                _response.ErrorMessage = new List<string>() { e.ToString() };
            }
            return _response;
        }
        [HttpPatch]
        [Route("contpatch/{id}")]
        public async Task<ActionResult<APIResponse>> EditarConPatch(int id, JsonPatchDocument<VillaUpdateDto> patchDto)
        {
            try
            {
                if (patchDto == null || id == 0) return NoContent();
                //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
                //if (villa == null) return NotFound();
                //patchDto.ApplyTo(villa, ModelState);
                var villa = await _villaRepo.Obtener(p => p.Id == id, tracked: false);
                if (!ModelState.IsValid) return BadRequest(ModelState);
                if (villa == null) return BadRequest();

                var convertir = _mapper.Map<VillaUpdateDto>(villa);
                patchDto.ApplyTo(convertir, ModelState);
                villa = _mapper.Map<Villa>(convertir);
                await _villaRepo.Actualizar(villa);
                return NoContent();
            }
            catch (Exception e)
            {
                _response.IsExitoso = false;
                _response.ErrorMessage = new List<string>() { e.ToString() };
            }
            return _response;
        }
    }

}
