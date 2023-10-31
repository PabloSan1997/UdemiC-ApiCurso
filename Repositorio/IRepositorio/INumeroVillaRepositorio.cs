using primeraApi.Modelos;

namespace primeraApi.Repositorio.IRepositorio
{
    public interface INumeroVillaRepositorio:IRepositorio<NumeroVilla>
    {
        Task <NumeroVilla> Actualizar(NumeroVilla entidad);
    }
}
