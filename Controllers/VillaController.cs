using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using primeraApi.Modelos.Dto;

namespace primeraApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<VillaDto> GetVillas ()
        {
            return new List<VillaDto>
            {
                new VillaDto{ Id=1, Nombre="Vista a la piscina"},
                new VillaDto{ Id=2, Nombre="Jamaica"}
            };
        }
    }
}
