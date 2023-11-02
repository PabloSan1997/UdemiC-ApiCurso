using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using primeraApi.Modelos;
using primeraApi.Modelos.Dto;
using primeraApi.Repositorio.IRepositorio;
using System.Net;

namespace primeraApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepo;
        private APIResponse _response;

        public UsuarioController(IUsuarioRepositorio usuarioRepo)
        {
            _usuarioRepo = usuarioRepo;
            _response = new();

        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Loggin([FromBody] LoginRequestDto modelo)
        {
            var loginResponse = await _usuarioRepo.Login(modelo);
            if (loginResponse == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _response.statusCode = HttpStatusCode.BadRequest;
                _response.IsExitoso = false;
                _response.ErrorMessage.Add("UserName o Password incorrectos");
                return BadRequest(_response);
            }
            _response.IsExitoso = true;
            _response.statusCode = HttpStatusCode.OK;
            _response.Resultado = loginResponse;
            return Ok(_response);
        }
        [HttpPost]
        [Route("registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegistroRequestDto modelo)
        {
            bool isUsuarioUnico = _usuarioRepo.IsUsuarioUnico(modelo.UserName);
            if (!isUsuarioUnico)
            {
                _response.statusCode = HttpStatusCode.BadRequest;
                _response.IsExitoso = false;
                _response.ErrorMessage.Add("Usuario ya existe");
                return BadRequest(_response);
            }
            var usuario = await _usuarioRepo.Registrar(modelo);
            if (usuario == null)
            {
                _response.statusCode = HttpStatusCode.BadRequest;
                _response.IsExitoso = false;
                _response.ErrorMessage.Add("Error al registrar usuario");
                return BadRequest(_response);
            }
            _response.statusCode = HttpStatusCode.OK;
            _response.IsExitoso = true;
            return Ok(_response);

        }
    }
}
