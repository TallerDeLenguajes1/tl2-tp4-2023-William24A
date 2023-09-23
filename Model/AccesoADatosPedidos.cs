using System.Text.Json;

namespace WebAPI;
public abstract class AccesoADatosPedido
{
    public virtual List<Pedido> LeerDatosPedido(string ruta)
    {
        return null;
    }
    public virtual void CargarDatosPedido(List<Pedido> listapedidos, string ruta)
    {
    }

}
public class AccesoPedidoCSV: AccesoADatosPedido
{
     public override List<Pedido>  LeerDatosPedido(string ruta)
    {
        List<Pedido> listapedido = new List<Pedido>();
        try
        {   
            using(StreamReader reader = new StreamReader(ruta))
            {
                while(!reader.EndOfStream)
                {
                    Pedido pedido = new Pedido();
                    string line = reader.ReadLine();
                    string[] dato = line.Split(',');
                    pedido = new Pedido(int.Parse(dato[0]),dato[1]);
                    pedido.Cliente.CambiarDatos(dato[2],dato[3],int.Parse(dato[4]), dato[5]);
                    listapedido.Add(pedido);
                }
            }  
            return listapedido;          
        }
        catch (Exception ex)
        {
            return listapedido;
        }

    }
    public override void CargarDatosPedido(List<Pedido> listapedidos, string ruta)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(ruta))
            {
                foreach (var pedido in listapedidos)
                {
                    writer.WriteLine($"{pedido.NumeroPedido},{pedido.Observacion},{pedido.Cliente.NombreCliente},{pedido.Cliente.Direccion},{pedido.Cliente.Telefono},{pedido.Cliente.Datosreferencia}");   
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
}

public class AccesoPedidoJSON: AccesoADatosPedido
{
    public override List<Pedido>  LeerDatosPedido(string ruta)
    {
        string pathJSON = Directory.GetCurrentDirectory()+"\\"+ruta;
        string Json = File.ReadAllText(pathJSON);
        return JsonSerializer.Deserialize<List<Pedido>>(Json);
    }
    public override void CargarDatosPedido(List<Pedido> listapedidos, string ruta)
    {
        string Json = JsonSerializer.Serialize<List<Pedido>>(listapedidos);
        string pathJSON = Directory.GetCurrentDirectory()+"\\"+ruta;
            using(StreamWriter sw = new StreamWriter(pathJSON, false)){
                sw.Write(Json);
                sw.Close();
            }
    }
}