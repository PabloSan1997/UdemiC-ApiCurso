using primeraApi.Modelos;
using primeraApi.Modelos.Datos;
using primeraApi.Repositorio.IRepositorio;

namespace primeraApi.Repositorio
{
    public class VillaRepositorio: Repositorio<Villa>, IVillaRepositorio
    {
        private readonly AplicationDbContext _db;

        public VillaRepositorio(AplicationDbContext db): base(db)
        {
            _db = db;
        }

        public async Task<Villa> Actualizar(Villa entidad)
        {
            entidad.ActualizacionFecha = DateTime.Now;
            _db.Villas.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
