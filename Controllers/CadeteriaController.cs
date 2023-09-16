using CadeteUtilizar;
using Microsoft.AspNetCore.Mvc;
using PedidoUtilizar;
using WebAPI;

namespace tl2_tp4_2023_William24A.Controllers;

[ApiController]
[Route("[controller]")]
public class CadeteriaController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private Cadeteria cadeteria;

    public CadeteriaController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
        cadeteria = Cadeteria.GetCadeteria();
    }

    [HttpGet]
    [Route("Pedido")]
    public ActionResult<List<Pedido>> GetPedidos()
    {
        return Ok(cadeteria.GetPedidos());
    }

    [HttpGet]
    [Route("Cadete")]
    public ActionResult<List<Cadete>> GetCadetes()
    {
        return Ok(cadeteria.GetCadetes());
    }
    [HttpGet]
    [Route("Informe")]
    public ActionResult<string> GetInforme()
    {
        return Ok();
    }
}
