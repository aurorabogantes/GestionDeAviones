using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GestionDeAviones.UI.Controllers
{
    public class GestionDeAvionesActivosController(ServicioApi servicioApi) : Controller
    {
        private readonly ServicioApi _servicioApis = servicioApi;

        public async Task<IActionResult> Index()
        {
            List<Model.Avion> lista;
            try
            {
                lista = await _servicioApis.ObtenerAvionesActivosAsync();
                ViewData["ProblemasAlConsultar"] = false;
                return View(lista);
            }
            catch
            {
                lista = [];
                ViewData["ProblemasAlConsultar"] = true;
                return View(lista);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Model.Avion avion)
        {
            try
            {
                await _servicioApis.AgregarAvionAsync(avion);
                ViewData["ProblemasAlInsertar"] = false;
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["ProblemasAlInsertar"] = true;
                return View();
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            Model.Avion avion;

            try
            {
                avion = await _servicioApis.ObtenerAvionPorIdAsync(id);
                return View(avion);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Model.Avion avion)
        {
            try
            {
                await _servicioApis.EditarAvionesAsync(avion);
                ViewData["ProblemasAlEditar"] = false;
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["ProblemasAlEditar"] = true;
                return View();
            }
        }
    }
}
