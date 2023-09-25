namespace WebAPI;

using ClienteUtilizar;
public enum Estado
{
    Encargado,
    Encamino,
    Entregado,
    Cancelado
}
public class Pedido
{
    private int numeroPedido;
    private string? observacion;
    private Cliente cliente;
    private Estado estado;
    private Cadete cadete;

    public int NumeroPedido { get => numeroPedido; set => numeroPedido = value; }
    public string? Observacion { get => observacion; set => observacion = value; }
    public Cliente Cliente { get => cliente; set => cliente = value; }
    public Estado Estado { get => estado; set => estado = value; }
    public Cadete Cadete { get => cadete; set => cadete = value; }

    public Pedido()
    {
        Cliente = new Cliente();
        Cadete = new Cadete();
    }
    public Pedido(int numeroPedido, string? observacion)
    {
        this.NumeroPedido = numeroPedido;
        this.Observacion = observacion;
        this.Cliente = new Cliente();
        this.Estado = Estado.Encargado;
        this.Cadete = new Cadete();
    }
    public Estado VerEstado()
    {
        return Estado;
    }
    public bool CambiarEstado()
    {
        switch(Estado)
        {
            case Estado.Encargado:
                Estado = Estado.Encamino;
                break;
            case Estado.Encamino:
                Estado = Estado.Entregado;
                Cadete.CambiarEstado();
                //cadete.Informe.AgregarPedido();
                break;
            default:
                return false;
        }
        return true;
    }
    public bool CancelarPedido()
    {
        Estado = Estado.Cancelado;
        return true;
    }
    public void EstadoEntregado() //Se utiliza en manejo de archivos para Leer datos
    {
        Estado = Estado.Entregado; 
    }
    public void CambiarDatosCadete(int id, string nombre, string direccion, int telefono)
    {
        this.Cadete.CambiarDatos(id,nombre,direccion,telefono);
    }
    public void CambiarDatosCliente(string nombreCliente, string direccion, int telefono, string datosreferencia)
    {
        this.Cliente.CambiarDatos(nombreCliente, direccion,telefono,datosreferencia);
    }

    public string InformePedido()
    {
        return $"Numero de pedido: {NumeroPedido}\nObservacion: {Observacion}\n"+Cliente.Informe()+Cadete.InformeCadete();
    }

}