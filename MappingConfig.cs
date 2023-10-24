using AutoMapper;
using primeraApi.Modelos;
using primeraApi.Modelos.Dto;

namespace primeraApi
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<Villa, VillaDto>();
            CreateMap<VillaDto, Villa>();

            CreateMap<Villa, VillaCreateDto>().ReverseMap();

            CreateMap<Villa, VillaUpdateDto>().ReverseMap();
        }
    }
}
