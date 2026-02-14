using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace GestionDeAviones.UI.Controllers
{
    public class GestionDeAvionesController(ServicioApi servicioApis) : Controller
    {
        private readonly ServicioApi _servicioApis = servicioApis;
        private const string apiKey = "123456";

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
            Model.Avion avion;
            try
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("X-API-KEY", apiKey);

                httpClient.BaseAddress = new Uri("https://localhost:7119");
                var response = await httpClient.GetAsync($"api/ServicioDeAviones/ObtengaElAvion?id={id}");
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
                avion = JsonSerializer.Deserialize<Model.Avion>(result);
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
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("X-API-KEY", apiKey);

                httpClient.BaseAddress = new Uri("https://localhost:7119");
                var json = JsonSerializer.Serialize(avion);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = httpClient.PostAsync("api/ServicioDeAviones/Agregue", content);

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
            Model.Avion avion;

            try
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("X-API-KEY", apiKey);

                httpClient.BaseAddress = new Uri("https://localhost:7119");
                var response = await httpClient.GetAsync($"api/ServicioDeAviones/ObtengaElAvion?id={id}");
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
                avion = JsonSerializer.Deserialize<Model.Avion>(result);
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
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("X-API-KEY", apiKey);

                httpClient.BaseAddress = new Uri("https://localhost:7119");

                var json = JsonSerializer.Serialize(avion);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = httpClient.PutAsync("api/ServicioDeAviones/EditeElAvion", content).Result;
                response.EnsureSuccessStatusCode();

                ViewData["ProblemasAlEditar"] = false;

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["ProblemasAlEditar"] = true;
                return View();
            }
        }

        public ActionResult Activar(int id)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("X-API-KEY", apiKey);

            try
            {
                httpClient.BaseAddress = new Uri("https://localhost:7119");
                var response = httpClient.PutAsync($"api/ServicioDeAviones/Active?id={id}", null).Result;
                response.EnsureSuccessStatusCode();
                ViewData["ProblemasAlActivar"] = false;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewData["ProblemasAlActivar"] = true;
                return RedirectToAction(nameof(Index));
            }
        }

        public ActionResult DesActivar(int id)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("X-API-KEY", apiKey);

            try
            {
                httpClient.BaseAddress = new Uri("https://localhost:7119");
                var response = httpClient.PutAsync($"api/ServicioDeAviones/DesActive?id={id}", null).Result;
                response.EnsureSuccessStatusCode();
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
