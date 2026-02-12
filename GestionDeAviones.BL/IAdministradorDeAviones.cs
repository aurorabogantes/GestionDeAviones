using GestionDeAviones.Model;

namespace GestionDeAviones.BL
{
    public interface IAdministradorDeAviones
    {
        Task ActiveAsync(int id);
        Task DesActiveAsync(int id);
        Task AgregueAsync(Avion avion);
        Task<IEnumerable<Avion>> ObtengaLaListaAsync();
        Task<IEnumerable<Avion>> ObtengaLaListaDeActivosAsync();
        Task<IEnumerable<Avion>> ObtentaLaListaDeInActivosAsync();
        Task<Avion?> ObtengaElAvionAsync(int id);
        Task EditeElAvionAsync(Avion avion);
    }
}
