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

    public int NumeroPedido { get => numeroPedido; }
    public string? Observacion { get => observacion; }
    public Cliente Cliente { get => cliente; }
    public Estado Estado { get => estado; }
    public Cadete Cadete { get => cadete;}
    public Pedido()
    {
        cliente = new Cliente();
        cadete = new Cadete();
    }
    public Pedido(int numeroPedido, string? observacion)
    {
        this.numeroPedido = numeroPedido;
        this.observacion = observacion;
        this.cliente = new Cliente();
        this.estado = Estado.Encargado;
        this.cadete = new Cadete();
    }
    public Estado VerEstado()
    {
        return Estado;
    }
    public bool CambiarEstado()
    {
        switch(estado)
        {
            case Estado.Encargado:
                estado = Estado.Encamino;
                break;
            case Estado.Encamino:
                estado = Estado.Entregado;
                cadete.CambiarEstado();
                //cadete.Informe.AgregarPedido();
                break;
            default:
                return false;
        }
        return true;
    }
    public bool CancelarPedido()
    {
        estado = Estado.Cancelado;
        return true;
    }
    public void EstadoEntregado() //Se utiliza en manejo de archivos para Leer datos
    {
        estado = Estado.Entregado; 
    }
    public void CambiarDatosCadete(int id, string nombre, string direccion, int telefono)
    {
        this.cadete.CambiarDatos(id,nombre,direccion,telefono);
    }
    public void CambiarDatosCliente(string nombreCliente, string direccion, int telefono, string datosreferencia)
    {
        this.cliente.CambiarDatos(nombreCliente, direccion,telefono,datosreferencia);
    }

    public string InformePedido()
    {
        return $"Numero de pedido: {numeroPedido}\nObservacion: {observacion}\n"+cliente.Informe()+cadete.InformeCadete();
    }

}