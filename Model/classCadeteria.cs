namespace WebAPI;
//using WebAPI;

public class Cadeteria 
{
    private static Cadeteria cadeteriaSingleton;

    public static Cadeteria GetCadeteria()
    {
        if(cadeteriaSingleton == null)
        {
            var accesoacadeteria =new AccesoCadeteriaJSON(); 
            cadeteriaSingleton = accesoacadeteria.LeerDatosCadeteria("CadeteriaJSON.json");
            cadeteriaSingleton.archivoCadete = new AccesoCadeteJSON();
            cadeteriaSingleton.archivoPedido = new AccesoPedidoJSON();
            cadeteriaSingleton.AsignarListaEmpleados();
            cadeteriaSingleton.AsignarListaPedido();
        }
        return cadeteriaSingleton;
    }
    private string nombre;
    private int telefono;
    private List<Cadete> listaempleados;
    private List<Pedido> listapedidos;
    private AccesoADatosCadeteria archivoCadeteria;
    private AccesoADatosCadete archivoCadete;
    private AccesoADatosPedido archivoPedido;

    public Cadeteria()
    {
        nombre ="";
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

    public string Nombre { get => nombre; set => nombre = value;}
    public int Telefono { get => telefono; set => telefono = value;}
    public AccesoADatosCadeteria ArchivoCadeteria { get => archivoCadeteria;}
    public AccesoADatosCadete ArchivoCadete { get => archivoCadete;}
    public AccesoADatosPedido ArchivoPedido { get => archivoPedido;}
    public bool CrearCadeteAgregar(int id, string nombre, string direccion, int telefono)
    {
        Cadete cadete = new Cadete(id,nombre,direccion,telefono);
        listaempleados.Add(cadete);
        return true;
    }
    public void EliminarCadete(int id)
    {
        listaempleados.RemoveAll(e => e.Id == id );
    }
    public bool CrearPedidoAgregar(int numeroPedido, string? observacion)
    {
        Pedido pedido = new Pedido(numeroPedido, observacion);
        listapedidos.Add(pedido);
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
        foreach (var pedido in listapedidos)
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
        foreach (var pedido in listapedidos)
        {
            if(pedido.NumeroPedido == idpedido)
            {
                foreach (var cadete in listaempleados)
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
        foreach (var pedido in listapedidos)
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
        foreach (var cadete in listaempleados)
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
    public void AsignarListaEmpleados()
    {
        listaempleados = archivoCadete.LeerDatosCadetes("CadetesJSON.json");
    }
    public void AsignarListaPedido()
    {
        listapedidos = archivoPedido.LeerDatosPedido("PedidoJSON.json");
    }
}
