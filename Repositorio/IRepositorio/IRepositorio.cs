using System.Linq.Expressions;
namespace primeraApi.Repositorio.IRepositorio
{
    public interface IRepositorio<T> where T:class
    {
        Task Crear(T entidad);
        Task<List<T>> ObtenerTodo(Expression<Func<T, bool>>? filtro=null);
        Task<T> Obtener(Expression<Func<T, bool>>? filtro = null, bool tracked=true);
        Task Eliminar(T entidad);
        Task Grabar();
    }
}
