using Microsoft.AspNetCore.Mvc;

namespace GestionDeAviones.UI.Controllers
{
    public class GestionDeAvionesInActivosController(ServicioApi servicioApi) : Controller)
    {
        readonly ServicioApi _servicioApi = servicioApi;
        // GET: GestionDeAvionesInActivosController
        public async Task<ActionResult> Index()
        {
            var listaDeInactivos = await _servicioApi.ObtenerAvionesInActivosAsync();
            return View(listaDeInactivos);
        }
    }
}
