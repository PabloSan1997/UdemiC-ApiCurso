using System.ComponentModel.DataAnnotations;

namespace primeraApi.Modelos.Dto
{
    public class NumeroVillaDtoUpdate
    {
        [Required]
        public int VillaNo { get; set; }
        [Required]
        public int VillaId { get; set; }
        public string DetalleEspecial { get; set; }
    }
}
