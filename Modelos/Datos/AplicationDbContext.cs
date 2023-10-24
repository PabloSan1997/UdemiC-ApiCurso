using Microsoft.EntityFrameworkCore;

namespace primeraApi.Modelos.Datos
{
    public class AplicationDbContext: DbContext
    {
        public DbSet<Villa> Villas { get; set; }
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) :base(options) {}

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            List<Villa> listaPorDefecto = new()
            {
                new Villa()
                {
                    Nombre="Villa San alejandro",
                    Id=1,
                    Detalle="detalle de la villa",
                    FechaCreacion=DateTime.Now,
                    ActualizacionFecha=DateTime.Now,
                    Amenidad="",
                    ImagenUrl="",
                    MetrosCuadrados=25,
                    Tarifa=200,
                    Ocupantes=54
                },
                new Villa()
                {
                    Nombre="No se que onda",
                    Id=2,
                    Detalle="Mira esto",
                    FechaCreacion=DateTime.Now,
                    ActualizacionFecha=DateTime.Now,
                    Amenidad="",
                    ImagenUrl="",
                    MetrosCuadrados=2534,
                    Tarifa=200543,
                    Ocupantes=544
                }
            };
            modelbuilder.Entity<Villa>().HasData(listaPorDefecto);
        }
    }
}
