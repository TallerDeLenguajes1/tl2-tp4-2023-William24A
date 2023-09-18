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
    [HttpPost("Agregar pedido")]
    public ActionResult AddPedido(int numeroPedido, string? observacion,string nombreCliente, string direccion, int telefono, string datosreferencia)
    {
        return Ok(!cadeteria.ExisteNumeroPedido(numeroPedido) && cadeteria.CrearPedidoAgregar(numeroPedido, observacion) && cadeteria.AsignarClienteAPedido(numeroPedido, nombreCliente, direccion, telefono, datosreferencia));
    }

    [HttpPut("Asignar")]
    public ActionResult AsignarPedido(int idpedido, int idcadete )
    {
        return Ok(cadeteria.AsignarCadeteAPedido(idcadete, idpedido));
    }
    [HttpPut("Cambio de Estado")]
    public ActionResult CambioEstado(int idpedido)
    {
        return Ok(cadeteria.ExisteNumeroPedido(idpedido) && cadeteria.CambiarEstado(idpedido));
    }
    [HttpPut("Asignar nuevo cadete")]
    public ActionResult AsignarNuevo(int idpedido, int idcadete)
    {
        return Ok(cadeteria.ExisteNumeroPedido(idpedido) && cadeteria.ExisteIDCadete(idcadete) && cadeteria.AsignarCadeteAPedido(idcadete, idpedido));
    }
}
