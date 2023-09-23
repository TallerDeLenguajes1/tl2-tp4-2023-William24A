namespace WebAPI;
//using WebAPI;

public class Cadeteria 
{
    private static Cadeteria cadeteriaSingleton;

    public static Cadeteria GetCadeteria()
    {
        if(cadeteriaSingleton == null)
        {
            cadeteriaSingleton = new Cadeteria();
            cadeteriaSingleton.CargarDatos("json");
        }
        return cadeteriaSingleton;
    }
    private string nombre;
    private int telefono;
    private List<Cadete> listaempleados;
    private List<Pedido> listapedidos;

    public Cadeteria()
    {
        listaempleados = new List<Cadete>();
        listapedidos = new List<Pedido>();
    }
    public Cadeteria(string nombre, int telefono)
    {
        this.nombre = nombre;
        this.telefono= telefono;
        listaempleados = new List<Cadete>();
        listapedidos = new List<Pedido>();
    }

    public string Nombre { get => nombre; }
    public int Telefono { get => telefono; }
    internal List<Cadete> Listaempleados { get => listaempleados; }
    internal List<Pedido> Listapedios {get => listapedidos;}
    private bool CargarDatos(string nombreclase)
    {
        if(nombreclase == "csv")
        {
            var archivo = new AccesoCadeteriaCSV();
            var archivoCSV = new AccesoCadeteCSV();
            var archivoPedido = new AccesoPedidoCSV();
            archivo.LeerDatosCadeteria("CadeteriaCSV.csv");
            cadeteriaSingleton.AsignarListaEmpleados(archivoCSV.LeerDatosCadetes("CadetesCSV.csv"));
            cadeteriaSingleton.AsignarListaPedido(archivoPedido.LeerDatosPedido("PedidoCSV.csv"));
            return true;
        }else if(nombreclase == "json")
        {
            var archivo = new AccesoCadeteriaJSON();
            var archivoJSON = new AccesoCadeteJSON();
            var archivoPedido = new AccesoPedidoJSON();
            archivo.LeerDatosCadeteria("CadeteriaJSON.json");
            cadeteriaSingleton.AsignarListaEmpleados(archivoJSON.LeerDatosCadetes("CadetesJSON.json"));
            cadeteriaSingleton.AsignarListaPedido(archivoPedido.LeerDatosPedido("PedidoJSON.json"));
            return true;
        }
        return false;
    }
    public bool CrearCadeteAgregar(int id, string nombre, string direccion, int telefono)
    {
        Cadete cadete = new Cadete(id,nombre,direccion,telefono);
        Listaempleados.Add(cadete);
        return true;
    }
    public void EliminarCadete(int id)
    {
        Listaempleados.RemoveAll(e => e.Id == id );
    }
    public bool CrearPedidoAgregar(int numeroPedido, string? observacion)
    {
        Pedido pedido = new Pedido(numeroPedido, observacion);
        Listapedios.Add(pedido);
        return true;
    }

    public List<Pedido> GetPedidos()
    {
        return listapedidos;
    }
    public List<Cadete> GetCadetes()
    {
        return listaempleados;
    }
    public bool CambiarEstado(int codigoPedido)
    {
        foreach (var pedido in Listapedios)
        {
            if(pedido.NumeroPedido == codigoPedido)
            {
                if(pedido.CambiarEstado())
                {
                    if(pedido.Estado == Estado.Entregado)
                    {
                        foreach (var cadete in listaempleados)
                        {
                            if(cadete.Id == pedido.Cadete.Id)
                            {
                                cadete.Informe.AgregarPedido(pedido);
                                return true;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }
    public int JornalACobrarCantidad(int idcadete)
    {
        int cont = 0;
        foreach (var pedido in listapedidos)
        {
            if(pedido.Cadete.Id == idcadete)
            {
                cont++;
            }
        }
        return cont;
    }
    public Pedido AsignarCadeteAPedido(int idcadete, int idpedido)
    {
        foreach (var pedido in Listapedios)
        {
            if(pedido.NumeroPedido == idpedido)
            {
                foreach (var cadete in Listaempleados)
                {
                    if(cadete.Id == idcadete)
                    {
                        pedido.CambiarDatosCadete(cadete.Id, cadete.Nombre, cadete.Direccion, cadete.Telefono);
                        cadete.CambiarEstado();
                        return pedido;;
                    }
                }
            }
        }
        return null;
    }
    public bool AsignarClienteAPedido(int idpedido,string nombreCliente, string direccion, int telefono, string datosreferencia)
    {
        foreach (var pedido in Listapedios)
        {
            if(pedido.NumeroPedido == idpedido)
            {
                pedido.CambiarDatosCliente(nombreCliente,direccion,telefono,datosreferencia);
                return true;
            }
        }
        return false;
    }
    public int EncontrarCadeteLibere(int idcadete)
    {
        foreach (var cadete in Listaempleados)
        {
            if(cadete.Id == idcadete)
            {
                if(cadete.EstadoCadete == EstadoCadete.Libre)
                {
                    return cadete.Id;
                }
            }
        }
        return 0;
    }

    public List<Pedido> RetornarListaEntregados()
    {
        List<Pedido> listaNueva = new List<Pedido>();
        foreach (var pedido in listapedidos)
        {
            if(pedido.Estado == Estado.Entregado)
            {
                listaNueva.Add(pedido);
            }
        }
        return listaNueva;
    }
    public bool ExisteCadete()
    {
        if(listaempleados.Count > 0)
        {
            return true;
        }
        return false;
    }
    public bool ExistePedido()
    {
        if(listapedidos.Count > 0)
        {
            return true;
        }
        return false;
    }
    public bool CancelarPedido(int numeroPedido)
    {
        foreach (var pedido in listapedidos)
        {
            if(pedido.NumeroPedido == numeroPedido)
            {
                pedido.CancelarPedido();
                return true;
            }
        }
        return false;
    }
    public string InformePedido(int idpedido)
    {
        string retornar="";
        foreach (var pedido in listapedidos)
        {
            if(pedido.NumeroPedido == idpedido)
            {
                retornar = pedido.InformePedido();
                retornar +="\n";
                return retornar;
            }
            
        }
        return retornar;
    }
    public string InformeCadete(int idcadete)
    {
        string retornar = "";
        foreach (var cadete in listaempleados)
        {
            if(cadete.Id == idcadete)
            {
                retornar = cadete.InformeCadete();
                retornar +="\n";
            }
            
        }
        return retornar;
    }
    public bool ExisteNumeroPedido(int idpedido)
    {
        foreach (var pedido in listapedidos)
        {
            if(pedido.NumeroPedido == idpedido)
            {
                return true;
            }
        }
        return false;
    }
    public bool ExisteIDCadete(int idcadete)
    {
        foreach (var cadete in listaempleados)
        {
            if(cadete.Id == idcadete)
            {
                return true;
            }
        }
        return false;
    }
    public List<Informe> ReturnInforme()
    {
        List<Informe> listaInforme = new List<Informe>();
            foreach (var cadete in listaempleados)
            {
                listaInforme.Add(cadete.Informe);
            }
        return listaInforme;
    }
    public void AsignarListaEmpleados(List<Cadete> listacadetesNueva)
    {
        listaempleados = listacadetesNueva;
    }
    public void AsignarListaPedido(List<Pedido> listaPedido)
    {
        listapedidos = listaPedido;
    }
}
