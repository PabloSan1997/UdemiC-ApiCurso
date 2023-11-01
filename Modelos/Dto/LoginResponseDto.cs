using primeraApi.Modelos;

namespace primeraApi.Modelos.Dto
{
    public class LoginResponseDto
    {
        public Usuario Usuario { get; set; }
        public string Token { get; set; }
    }
}
