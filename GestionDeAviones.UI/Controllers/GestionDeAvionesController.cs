using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GestionDeAviones.UI.Controllers
{
    public class GestionDeAvionesController(ServicioApi servicioApis) : Controller
    {
        private readonly ServicioApi _servicioApis = servicioApis;
        private const string apiKey = "123456";

        public async Task<IActionResult> Index(string nombre)
        {
            List<Model.Avion> lista;

            try
            {
                lista = await _servicioApis.ObtenerAvionesAsync();
                ViewData["ProblemasAlConsultar"] = false;
            }
            catch (Exception ex)
            {
                lista = new List<Model.Avion>();
                ViewData["ProblemasAlConsultar"] = true;
            }

            if(nombre is null)
            {
                return View(lista);
            }

            else
            {
                List<Model.Avion> listaFiltrada;
                listaFiltrada = lista.Where(x => x.Nombre.Contains(nombre)).ToList();
                return View(listaFiltrada);
            }
        }
    }
}
