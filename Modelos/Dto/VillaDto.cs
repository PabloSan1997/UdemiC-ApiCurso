using System.ComponentModel.DataAnnotations;

namespace primeraApi.Modelos.Dto
{
    public class VillaDto
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Detalle { get; set; }
        [Required]
        public Double Tarifa { get; set; }
        public int Ocupantes { get; set; }
        public double MetrosCuadrados { get; set; }
        public string ImagenUrl { get; set; }
        public string Amenidad { get; set; }
    }
}
