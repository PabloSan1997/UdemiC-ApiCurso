using primeraApi.Modelos;
using primeraApi.Modelos.Datos;
using primeraApi.Repositorio.IRepositorio;

namespace primeraApi.Repositorio
{
    public class NumeroVillaRepositorio: Repositorio<NumeroVilla>, INumeroVillaRepositorio
    {
        private readonly AplicationDbContext _db;

        public NumeroVillaRepositorio(AplicationDbContext db): base(db)
        {
            _db = db;
        }

        public async Task<NumeroVilla> Actualizar(NumeroVilla entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;
            _db.NumeroVillas.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
