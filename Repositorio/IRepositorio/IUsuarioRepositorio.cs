using primeraApi.Modelos;
using primeraApi.Modelos.Dto;

namespace primeraApi.Repositorio.IRepositorio
{
    public interface IUsuarioRepositorio
    {
        bool IsUsuarioUnico(string userName);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<Usuario> Registrar(RegistroRequestDto registroRequestDto);
    }
}
