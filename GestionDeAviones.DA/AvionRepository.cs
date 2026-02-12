using GestionDeAviones.BL;
using GestionDeAviones.Model;
using Microsoft.EntityFrameworkCore;

namespace GestionDeAviones.DA
{
    public class AvionRepository : IAvionRepository
    {
        private readonly DBContexto _context;

        public AvionRepository(DBContexto context)
        {
            _context = context;
        }
        public async Task ActivarAsync(int id)
        {
            var avion = await ObtenerPorIdAsync(id);
            if ((avion != null))
            {
                avion.Estado = Estado.Activo;
                _context.Aviones.Update(avion);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ActualizarAsync(Avion avion)
        {
            _context.Aviones.Update(avion);
            await _context.SaveChangesAsync();
        }

        public async Task AgregarAsync(Avion avion)
        {
            await _context.Aviones.AddAsync(avion);
            await _context.SaveChangesAsync();
        }

        public async Task DesActivarAsync(int id)
        {
            var avion = await ObtenerPorIdAsync(id);
            if(avion != null)
            {
                avion.Estado = Estado.InActivo;
                _context.Aviones.Update(avion);
                await _context.SaveChangesAsync();
            }
        }

        public async Task EliminarAsync(int id)
        {
            var avion = await ObtenerPorIdAsync(id);
            if (avion != null)
            {
                _context.Aviones.Remove(avion);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Avion>> ObtenerActivosAsync()
        {
            return await _context.Aviones.Where(a => a.Estado == Estado.Activo).ToListAsync();
        }

        public async Task<IEnumerable<Avion>> ObtenerAsync()
        {
            return await _context.Aviones.ToListAsync();
        }

        public async Task<IEnumerable<Avion>> ObtenerInActivosAsync()
        {
            return await _context.Aviones.Where(a => a.Estado == Estado.InActivo).ToListAsync();
        }

        public async Task<Avion?> ObtenerPorIdAsync(int id)
        {
            return await _context.Aviones.FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
