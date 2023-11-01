using System.ComponentModel.DataAnnotations;

namespace primeraApi.Modelos
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string Rol { get;set; }
    }
}
