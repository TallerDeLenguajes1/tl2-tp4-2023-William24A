using Microsoft.AspNetCore.Mvc;
using WebAPI;

namespace tl2_tp4_2023_William24A.Controllers;

[ApiController]
[Route("[controller]")]
public class CadeteriaController : ControllerBase
{
    private readonly ILogger<CadeteriaController> _logger;
    private Cadeteria cadeteria;

    public CadeteriaController(ILogger<CadeteriaController> logger)
    {
        _logger = logger;
        cadeteria = Cadeteria.GetCadeteria();
    }

    [HttpGet]
    [Route("Pedido")]
    public ActionResult<List<Pedido>> GetPedidos()
    {
        cadeteria.AsignarListaPedido(); //esto para todos los llamados
        if(cadeteria.ExistePedido())
        {
            return Ok(cadeteria.GetPedidos());
        }
        return NotFound(false);
    }

    [HttpGet]
    [Route("Pedido por id")]
    public ActionResult<Pedido> GetPedidoId(int idpedido)
    {
        if(cadeteria.ExisteNumeroPedido(idpedido))
        {
            if(cadeteria.GetPedidoID(idpedido) == null)
            {
                return NotFound(false);
            }
            return Ok(cadeteria.GetPedidoID(idpedido));
        }
        return NotFound(false);
    }    

    [HttpGet]
    [Route("Cadete")]
    public ActionResult<List<Cadete>> GetCadetes()
    {
        if(cadeteria.ExisteCadete())
        {
            return Ok(cadeteria.GetCadetes());
        }
        return NotFound(false);
    }

    [HttpGet]
    [Route("Cadete por id")]
    public ActionResult<Cadete> GetCadeteId(int idcadete)
    {
        if(cadeteria.ExisteIDCadete(idcadete))
        {
            if(cadeteria.GetCadeteId(idcadete) == null)
            {
                return NotFound(false);
            }
            return Ok(cadeteria.GetCadeteId(idcadete));
        }
        return NotFound(false);
    }

    [HttpGet]
    [Route("Informe")]
    public ActionResult<Informe> GetInforme()
    {
        if(cadeteria.ExistePedido())
        {
            return Ok(cadeteria.ReturnInforme());
        }
        return NotFound(false);
    }

    [HttpPost("Agregar cadete")]
    public ActionResult<List<Cadete>> AddCadete(int id, string nombre, string direccion, int telefono)
    {
        if(!cadeteria.ExisteIDCadete(id))
        {
            cadeteria.CrearCadeteAgregar(id,nombre,direccion,telefono);
            cadeteria.ArchivoCadete.CargarDatosCadetes(cadeteria,"CadetesJSON.json");
            return Created("",cadeteria.GetCadetes());
        }
        return NotFound("Ya existe el id");
    }
    
    [HttpPost("Agregar pedido")]
    public ActionResult<List<Pedido>> AddPedido(int numeroPedido, string? observacion,string nombreCliente, string direccion, int telefono, string datosreferencia)
    {
        if(!cadeteria.ExisteNumeroPedido(numeroPedido))
        {
            cadeteria.CrearPedidoAgregar(numeroPedido, observacion);  
            cadeteria.AsignarClienteAPedido(numeroPedido, nombreCliente, direccion, telefono, datosreferencia);
        }
        cadeteria.ArchivoPedido.CargarDatosPedido(cadeteria.GetPedidos(),"PedidoJSON.json");
        return Created("", cadeteria.GetPedidos());
        //return Ok(!cadeteria.ExisteNumeroPedido(numeroPedido) && cadeteria.CrearPedidoAgregar(numeroPedido, observacion) && cadeteria.AsignarClienteAPedido(numeroPedido, nombreCliente, direccion, telefono, datosreferencia));
    }

    [HttpPut("Asignar")]
    public ActionResult<string> AsignarPedido(int idpedido, int idcadete )
    {
        if(cadeteria.ExisteNumeroPedido(idpedido) && cadeteria.ExisteIDCadete(idcadete))
        {
            if(cadeteria.EncontrarCadeteLibere(idcadete) != 0)
            {
                cadeteria.AsignarCadeteAPedido(idcadete, idpedido);
                cadeteria.ArchivoPedido.CargarDatosPedido(cadeteria.GetPedidos(),"PedidoJSON.json");
                return Ok("Pedido asignado");
            }
            else
            {
                return NotFound("No esta libre el cadete");
            }
        }
        return NotFound(false);
    }
    [HttpPut("Cambio de Estado")]
    public ActionResult CambioEstado(int idpedido)
    {
        if(cadeteria.ExisteNumeroPedido(idpedido))
        {
            cadeteria.CambiarEstado(idpedido);
            cadeteria.ArchivoPedido.CargarDatosPedido(cadeteria.GetPedidos(),"PedidoJSON.json");
            return Ok("Estado modificado");    
        }
        return  NotFound("no existe el id pedido.");
    }
    [HttpPut("Asignar nuevo cadete")]
    public ActionResult<string> AsignarNuevo(int idpedido, int idcadete)
    {
        if(cadeteria.ExisteNumeroPedido(idpedido) && cadeteria.ExisteIDCadete(idcadete))
        {
            if(cadeteria.EncontrarCadeteLibere(idcadete) != 0)
            {
                cadeteria.AsignarCadeteAPedido(idcadete, idpedido);
                cadeteria.ArchivoPedido.CargarDatosPedido(cadeteria.GetPedidos(),"PedidoJSON.json");
                return Ok("Pedido asignado");
            }
            else
            {
                return NotFound("No esta libre el cadete");
            }
        }
        return NotFound("Pedido no existe o cadete no existe");
    }
}