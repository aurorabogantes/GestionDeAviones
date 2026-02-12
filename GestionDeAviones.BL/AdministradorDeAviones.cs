using GestionDeAviones.Model;

namespace GestionDeAviones.BL
{
    public class AdministradorDeAviones : IAdministradorDeAviones
    {
        private readonly IAvionRepository _avionRepository;

        public AdministradorDeAviones(IAvionRepository avionRepository)
        {
            _avionRepository = avionRepository;
        }
        public async Task ActiveAsync(int id)
        {
            await _avionRepository.ActivarAsync(id);
        }

        public async Task AgregueAsync(Avion avion)
        {
            avion.Estado = Estado.Activo;
            await _avionRepository.AgregarAsync(avion);
        }

        public async Task DesActiveAsync(int id)
        {
            await _avionRepository.DesActivarAsync(id);
        }

        public async Task EditeElAvionAsync(Avion avion)
        {
            var elAvionAModificar = await _avionRepository.ObtenerPorIdAsync(avion.Id);
            if(elAvionAModificar != null)
            {
                elAvionAModificar.Nombre = avion.Nombre;
                elAvionAModificar.Modelo = avion.Modelo;
                await _avionRepository.ActualizarAsync(elAvionAModificar);
            }
        }

        public async Task<Avion?> ObtengaElAvionAsync(int id)
        {
            return await _avionRepository.ObtenerPorIdAsync(id);
        }

        public async Task<IEnumerable<Avion>> ObtengaLaListaAsync()
        {
            return await _avionRepository.ObtenerAsync();
        }

        public async Task<IEnumerable<Avion>> ObtengaLaListaDeActivosAsync()
        {
            return await _avionRepository.ObtenerActivosAsync();
        }

        public async Task<IEnumerable<Avion>> ObtentaLaListaDeInActivosAsync()
        {
            return await _avionRepository.ObtenerInActivosAsync();
        }
    }
}
