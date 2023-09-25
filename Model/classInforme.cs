namespace WebAPI;

public class Informe
{
    private List<Pedido> pedidosEntregados;
    private double ganancia;
    private int cantidadEntregados;

    public Informe()
    {
        PedidosEntregados = new List<Pedido>();
        this.Ganancia = 0;
        this.CantidadEntregados = 0;
    }

    public List<Pedido> PedidosEntregados { get => pedidosEntregados; set => pedidosEntregados = value; }
    public double Ganancia { get => ganancia; set => ganancia = value; }
    public int CantidadEntregados { get => cantidadEntregados; set => cantidadEntregados = value; }

    public Pedido AgregarPedido(Pedido pedido)
    {
        PedidosEntregados.Add(pedido);
        Ganancia = PedidosEntregados.Count * 500;
        CantidadEntregados = PedidosEntregados.Count;
        return pedido;
    }
}