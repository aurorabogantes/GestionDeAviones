using Microsoft.AspNetCore.Mvc;

namespace GestionDeAviones.SI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicioDeAvionesController : ControllerBase
    {
        private readonly IAdministradorDeAviones _admin;

        public ServicioDeAvionesController(IAdministradorDeAviones admin)
        {
            _admin = admin;
        }

        [HttpGet("ObtengaLaLista")]
        public async Task<ActionResult<IEnumerable<Avion>>> ObtengaLaLista()
        {
            var lista = await _admin.ObtengaLaListaAsync();
            return Ok(lista);
        }
    }
}
