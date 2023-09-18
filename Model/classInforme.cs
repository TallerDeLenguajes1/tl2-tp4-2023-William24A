namespace WebAPI;
using PedidoUtilizar;
public class Informe
{
    private List<Pedido> pedidosEntregados;
    private double ganancia;
    private int cantidadEntregados;

    public Informe()
    {
        pedidosEntregados = new List<Pedido>();
        this.ganancia = 0;
        this.cantidadEntregados = 0;
    }

    public List<Pedido> PedidosEntregados { get => pedidosEntregados;}
    public double Ganancia { get => ganancia; }
    public int CantidadEntregados { get => cantidadEntregados;}
    
    public Pedido AgregarPedido(Pedido pedido)
    {
        pedidosEntregados.Add(pedido);
        ganancia = pedidosEntregados.Count * 500;
        cantidadEntregados = pedidosEntregados.Count;
        return pedido;
    }
}