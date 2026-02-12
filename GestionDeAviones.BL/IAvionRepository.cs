using GestionDeAviones.Model;

namespace GestionDeAviones.BL
{
    public interface IAvionRepository
    {
        Task<Avion?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Avion>> ObtenerAsync();
        Task AgregarAsync(Avion avion);
        Task ActualizarAsync(Avion avion);
        Task EliminarAsync(int id);
        Task ActivarAsync(int id);
        Task DesActivarAsync(int id);
        Task<IEnumerable<Avion>> ObtenerActivosAsync();
        Task<IEnumerable<Avion>> ObtenerInActivosAsync();
    }
}
