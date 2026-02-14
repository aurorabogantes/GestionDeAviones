using GestionDeAviones.Model;
using System.Text.Json;

namespace GestionDeAviones.UI
{
    public class ServicioApi
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public ServicioApi(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<Avion>> ObtenerAvionesAsync()
        {
            var client = _httpClientFactory.CreateClient("AvionesApi");
            var response = await client.GetAsync("api/ServicioDeAviones/ObtengaLaLista");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var lista = JsonSerializer.Deserialize<List<Avion>>(result, _jsonOptions) ?? [];
            return lista;
        }

        public async Task<List<Avion>> ObtenerAvionesActivosAsync()
        {
            var client = _httpClientFactory.CreateClient("AvionesApi");
            var response = await client.GetAsync("api/ServicioDeAviones/ObtengaLaListaDeActivos");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var lista = JsonSerializer.Deserialize<List<Avion>>(result, _jsonOptions) ?? [];
            return lista;
        }

        public async Task<List<Avion>> ObtenerAvionesInActivosAsync()
        {
            var client = _httpClientFactory.CreateClient("AvionesApi");
            var response = await client.GetAsync("api/ServicioDeAviones/ObtengaLaListaDeInActivos");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var lista = JsonSerializer.Deserialize<List<Avion>>(result, _jsonOptions) ?? [];
            return lista;
        }

        public async Task<Avion?> ObtenerAvionPorIdAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("AvionesApi");
            var response = await client.GetAsync($"api/ServicioDeAviones/ObtengaElAvion?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var avion = JsonSerializer.Deserialize<Avion>(result, _jsonOptions);
                return avion;
            }
            return null;
        }

        public async Task AgregarAvionAsync(Avion avion)
        {
            var client = _httpClientFactory.CreateClient("AvionesApi");
            var json = JsonSerializer.Serialize(avion);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/ServicioDeAviones/Agregue", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task EditarAvionesAsync(Avion avion)
        {
            var client = _httpClientFactory.CreateClient("AvionesApi");
            var json = JsonSerializer.Serialize(avion);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PutAsync("api/ServicioDeAviones/EditeElAvion", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task ActivarAvionAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("AvionesApi");
            var response = await client.PutAsync($"api/ServicioDeAviones/Active?id={id}", null);
            response.EnsureSuccessStatusCode();
        }

        public async Task DesActivarAvionAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("AvionesApi");
            var response = await client.PutAsync($"api/ServicioDeAviones/DesActive?id={id}", null);
            response.EnsureSuccessStatusCode();
        }
    }
}
