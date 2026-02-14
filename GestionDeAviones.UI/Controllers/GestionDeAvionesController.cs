using Microsoft.AspNetCore.Mvc;

namespace GestionDeAviones.UI.Controllers
{
    public class GestionDeAvionesController(ServicioApi servicioApis) : Controller
    {
        private readonly ServicioApi _servicioApis = servicioApis;

        // GET: GestionDeAvionesController
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

        // GET: GestionDeAvionesController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var avion = await _servicioApis.ObtenerAvionPorIdAsync(id);
                return View(avion);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        //GET: GestionDeAvionesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GestionDeAvionesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Model.Avion avion)
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

        // GET: GestionDeAvionesController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var avion = await _servicioApis.ObtenerAvionPorIdAsync(id);
                return View(avion);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // POST: GestionDeAvionesController/Edit/5
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

        public async Task<ActionResult> Activar(int id)
        {
            try
            {
                await _servicioApis.ActivarAvionAsync(id);
                ViewData["ProblemasAlActivar"] = false;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewData["ProblemasAlActivar"] = true;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<ActionResult> DesActivar(int id)
        {
            try
            {
                await _servicioApis.DesActivarAvionAsync(id);
                ViewData["ProblemasAlDesaAtivar"] = false;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewData["ProblemasAlDesaAtivar"] = true;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
