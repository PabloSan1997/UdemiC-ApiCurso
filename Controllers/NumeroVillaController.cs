using AutoMapper;
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
    public class NumeroVillaController : ControllerBase
    {
        private readonly ILogger<NumeroVillaController> _logger;
        private readonly IVillaRepositorio _villaRepo;
        public readonly INumeroVillaRepositorio _numerRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public NumeroVillaController(ILogger<NumeroVillaController> logger, IVillaRepositorio villaRepo, IMapper mapper, INumeroVillaRepositorio numerRepo)
        {
            _mapper = mapper;
            _logger = logger;
            _villaRepo = villaRepo;
            _response = new();
            _numerRepo = numerRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetNumeroVillas()
        {
            var villas = await _numerRepo.ObtenerTodo();
            var mostrar = _mapper.Map<IEnumerable<NumeroVillaDto>>(villas);
            _response.Resultado = mostrar;
            _response.statusCode = HttpStatusCode.OK;
            return Ok(_response);
        }
        [HttpGet("id:int", Name = "GetNumeroVilla")]
        public async Task<ActionResult<APIResponse>> GetNumeroVilla(int id)
        {
            try
            {
                //var villa = VillaStore.villaList.FirstOrDefault(p => p.Id == id);
                var villa = await _numerRepo.Obtener(p => p.VillaNo == id);
                if (villa == null)
                {
                    _logger.LogError("Error con el id");
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                };
                var mostrar = _mapper.Map<NumeroVillaDto>(villa);
                _response.Resultado = mostrar;
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<APIResponse>> CrearNumeroVilla([FromBody] NumeroVillaDtoCreate nuevaVilla)
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
                var verVillaNo = await _numerRepo.Obtener(p => p.VillaNo == nuevaVilla.VillaNo);
                if (verVillaNo != null)
                {
                    ModelState.AddModelError("NombreExiste", "La Villa con ese nombre ya existe");
                    _response.Resultado = ModelState;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                if (await _villaRepo.Obtener(v => v.Id == nuevaVilla.VillaId) == null)
                {
                    ModelState.AddModelError("Clave foranea", "El ide de Villa no existe");
                    return BadRequest(ModelState);
                }
                
                var agregar = _mapper.Map<NumeroVilla>(nuevaVilla);
                agregar.FechaCreacion = DateTime.Now;
                agregar.FechaActualizacion = DateTime.Now;
                await _numerRepo.Crear(agregar);
                _response.Resultado = agregar;
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
        public async Task<ActionResult<APIResponse>> DeleteNumeroVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var villa = await _numerRepo.Obtener(p => p.VillaNo == id);
                if (villa == null)
                {
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.Resultado = "No se encontro elemento con ese id";
                    return BadRequest(_response);
                }

                var elemento = _mapper.Map<NumeroVilla>(villa);
                await _numerRepo.Eliminar(elemento);

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
        public async Task<ActionResult<APIResponse>> Editar(int id, [FromBody] NumeroVillaDtoUpdate editarVilla)
        {
            try
            {
                if (id == 0 || editarVilla == null) return BadRequest();
                //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
                //if (villa == null) return NotFound();
                //villa.Nombre = editarVilla.Nombre;
                //villa.Ocupantes = editarVilla.Ocupantes;
                var villa = await _numerRepo.Obtener(p => p.VillaNo == id, tracked: false);
                if (villa == null) return NotFound();
                if (await _villaRepo.Obtener(p => p.Id == editarVilla.VillaId) == null)
                {
                    ModelState.AddModelError("Clave foranea", "El ide de Villa no existe");
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid) return BadRequest();

                var guardar = _mapper.Map<NumeroVilla>(editarVilla);
                await _numerRepo.Actualizar(guardar);

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
