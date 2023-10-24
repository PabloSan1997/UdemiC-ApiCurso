﻿using System.ComponentModel.DataAnnotations;

namespace primeraApi.Modelos.Dto
{
    public class VillaUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public string Detalle { get; set; }
        [Required]
        public Double Tarifa { get; set; }
        public int Ocupantes { get; set; }
        [Required]
        public double MetrosCuadrados { get; set; }
        [Required]
        public string ImagenUrl { get; set; }
        public string Amenidad { get; set; }
    }
}
