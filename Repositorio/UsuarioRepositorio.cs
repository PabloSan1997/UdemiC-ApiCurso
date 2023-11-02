using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using primeraApi.Modelos;
using primeraApi.Modelos.Datos;
using primeraApi.Modelos.Dto;
using primeraApi.Repositorio.IRepositorio;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace primeraApi.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly AplicationDbContext _db;
        private string secretKey;
        public UsuarioRepositorio(AplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        }
        public bool IsUsuarioUnico(string userName)
        {
            var usuarios = _db.Usuarios.FirstOrDefault(u => u.UserName.ToLower() == userName.ToLower());
            return usuarios == null;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var usuario = await _db.Usuarios.FirstOrDefaultAsync(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower() && u.Password == loginRequestDto.Password);
            if (usuario == null)
            {
                return new LoginResponseDto() 
                { 
                    Token="",
                    Usuario=null
                };
            }
            var tokenHeandle = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Role, usuario.Rol)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHeandle.CreateToken(tokenDescriptor);
            LoginResponseDto loginResponseDto = new()
            {
                Token = tokenHeandle.WriteToken(token),
                Usuario = usuario
            };
            return loginResponseDto;
        }

        public async Task<Usuario> Registrar(RegistroRequestDto registroRequestDto)
        {
            Usuario usuario = new()
            {
                UserName = registroRequestDto.UserName,
                Password = registroRequestDto.Password,
                Nombre = registroRequestDto.Nombre,
                Rol = registroRequestDto.Rol
            };
            await _db.Usuarios.AddAsync(usuario);
            await _db.SaveChangesAsync();
            usuario.Password = "";
            return usuario;
        }
    }
}
