using System.ComponentModel.DataAnnotations;

namespace primeraApi.Modelos.Dto
{
    public class VillaDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Nombre { get; set; }
    }
}
