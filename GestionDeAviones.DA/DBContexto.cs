using GestionDeAviones.Model;
using Microsoft.EntityFrameworkCore;

namespace GestionDeAviones.DA
{
    public class DBContexto : DbContext
    {
        public DBContexto(DbContextOptions<DBContexto> options) : base(options)
        {
        }

        public DbSet<Avion> Aviones { get; set; }
    }
}
