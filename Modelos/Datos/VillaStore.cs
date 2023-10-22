using primeraApi.Modelos.Dto;

namespace primeraApi.Modelos.Datos
{
    public static class VillaStore
    {
        public static List<VillaDto> villaList = new()
        {
            new VillaDto{Id=1, Nombre="Juan" },
            new VillaDto {Id=2, Nombre="Pedro"},
        };
    }
}
