using primeraApi.Modelos;

namespace primeraApi.Repositorio.IRepositorio
{
    public interface IVillaRepositorio:IRepositorio<Villa>
    {
        Task <Villa> Actualizar(Villa entidad);
    }
}
