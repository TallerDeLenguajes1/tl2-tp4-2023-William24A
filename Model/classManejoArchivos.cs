
using CadeteUtilizar;
using System.Text;
using System.Text.Json;

namespace WebAPI;
public abstract class AccesoADatos
{
    public virtual Cadeteria LeerDatosCadeteria(string ruta)
    {
        return null;
    }
    public virtual List<Cadete> LeerDatosCadetes(string ruta)
    {
        return null;
    }
    public virtual void CargarDatosCadeterias(Cadeteria cadeteria, string ruta)
     {
     }
    public virtual void CargarDatosCadetes(Cadeteria cadeteria, string ruta)
    {
    }
    public virtual bool ExisteArchivo(string ruta)
    {
        return false;
    }
    public virtual List<Pedido> LeerInforme(string ruta)
    {
        return null;
    }
    public virtual void CargarInforme(List<Pedido> listaPedidos, string ruta)
    {
    }
}
class AccesoCSV: AccesoADatos
{
    public override Cadeteria LeerDatosCadeteria(string ruta)
    {
        Cadeteria cadeteria = new Cadeteria();
        try
        {   
            using(StreamReader reader = new StreamReader(ruta))
            {
                while(!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] dato = line.Split(',');
                    cadeteria = new Cadeteria(dato[0], int.Parse(dato[1]));
                }
            }  
            return cadeteria;          
        }
        catch (Exception ex)
        {
            return cadeteria;
        }

    }
    public override List<Cadete> LeerDatosCadetes(string ruta)
    {
        List<Cadete> listacadete = new List<Cadete>();
        try
        {   
            using(StreamReader reader = new StreamReader(ruta))
            {
                while(!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] dato = line.Split(',');
                    Cadete cadete = new Cadete(int.Parse(dato[0]),dato[1],dato[2],int.Parse(dato[3]));
                    listacadete.Add(cadete);
                }
            }  
            return listacadete;          
        }
        catch (Exception ex)
        {
            return listacadete;
        }

    }
    public override void CargarDatosCadeterias(Cadeteria cadeteria, string ruta)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(ruta))
            {
                    writer.WriteLine($"{cadeteria.Nombre},{cadeteria.Telefono}");
            }
        }
        catch (Exception ex)
        {
        }
    }

    public override void CargarDatosCadetes(Cadeteria cadeteria, string ruta)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(ruta))
            {
                foreach (var item in cadeteria.Listaempleados)
                {
                    writer.WriteLine($"{item.Id},{item.Nombre},{item.Direccion},{item.Telefono}");
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    public override bool ExisteArchivo(string ruta)
    {
        if (File.Exists(ruta))
        {
            if (!string.IsNullOrWhiteSpace(File.ReadAllText(ruta)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

   public override List<Pedido> LeerInforme(string ruta)
{
    List<Pedido> listaPedido = new List<Pedido>();
    
    try
    {
        using (StreamReader reader = new StreamReader(ruta))
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] dato = line.Split(',');
                
                if (dato.Length >= 9) // Asegura que hay suficientes campos en la línea
                {
                    Pedido pedido = new Pedido(int.Parse(dato[0]),dato[1]);
                    
                    // Crea una instancia de Cliente y asigna sus propiedades
                    pedido.CambiarDatosCliente(dato[2],dato[3],int.Parse(dato[4]),dato[5]);
                    pedido.CambiarDatosCadete(int.Parse(dato[7]),dato[8],dato[9],int.Parse(dato[10]));
                    pedido.EstadoEntregado();

                    listaPedido.Add(pedido);
                }
            }
        }
        return listaPedido;
    }
    catch (Exception ex)
    {
        return listaPedido;
    }
}
    public override void CargarInforme(List<Pedido> listaPedidos, string ruta)
    {
        try
        {
            using (FileStream fs = new FileStream(ruta, FileMode.Append, FileAccess.Write))
            {
                
                foreach (var pedido in listaPedidos)
                {
                    
                    var data = $"{pedido.NumeroPedido},{pedido.Observacion},{pedido.Cliente.NombreCliente},{pedido.Cliente.Direccion},{pedido.Cliente.Telefono},{pedido.Cliente.Datosreferencia},{pedido.Estado},{pedido.Cadete.Id},{pedido.Cadete.Nombre},{pedido.Cadete.Direccion},{pedido.Cadete.Telefono}\n";   
                    byte[] bytes = Encoding.UTF8.GetBytes(data);
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
}
class AccesoJSON: AccesoADatos
{
    public override Cadeteria LeerDatosCadeteria(string ruta)
    {
        Cadeteria cadeteria = null;

        string pathJSON = Directory.GetCurrentDirectory()+"\\"+ruta;
        string Json = File.ReadAllText(pathJSON);
        cadeteria = JsonSerializer.Deserialize<Cadeteria>(Json);

        return cadeteria;
    }
    public override List<Cadete> LeerDatosCadetes(string ruta)
    {
        List<Cadete> listacadete = new List<Cadete>();
        string pathJSON = Directory.GetCurrentDirectory()+"\\"+ruta;
        string Json = File.ReadAllText(pathJSON); //Leer archivo y guardar
        foreach(var cadete in JsonSerializer.Deserialize<List<Cadete>>(Json)) // aclaracion de lista
        {
            listacadete.Add(cadete);
        } 
        return listacadete;
    }
    public override void CargarDatosCadeterias(Cadeteria cadeteria, string ruta)
    {
        string Json = JsonSerializer.Serialize<Cadeteria>(cadeteria);
        string pathJSON = Directory.GetCurrentDirectory()+"\\"+ruta;
            using(StreamWriter sw = new StreamWriter(pathJSON, false)){
                sw.Write(Json);
                sw.Close();
            }
    }
    public override void CargarDatosCadetes(Cadeteria cadeteria, string ruta)
    {
        string Json = JsonSerializer.Serialize<List<Cadete>>(cadeteria.Listaempleados);
        string pathJSON = Directory.GetCurrentDirectory()+"\\"+ruta;
            using(StreamWriter sw = new StreamWriter(pathJSON, false)){
                sw.Write(Json);
                sw.Close();
            }
    }
    public override bool ExisteArchivo(string ruta)
    {
        if (File.Exists(ruta))
        {
            if (!string.IsNullOrWhiteSpace(File.ReadAllText(ruta)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    public override List<Pedido> LeerInforme(string ruta)
    {
        List<Pedido> listaPedidos = new List<Pedido>();

        try
        {
            string Json = File.ReadAllText(ruta); // Lee el archivo JSON directamente desde la ruta proporcionada
            listaPedidos = JsonSerializer.Deserialize<List<Pedido>>(Json);
        }
        catch (FileNotFoundException ex)
        {
        }

        return listaPedidos;
    }
   public override void CargarInforme(List<Pedido> listaPedidos, string ruta)
    {
        try
        {
            string Json = JsonSerializer.Serialize(listaPedidos);
            File.WriteAllText(ruta, Json); // Escribe el JSON directamente en el archivo en la ruta proporcionada
        }
        catch (JsonException ex)
        {
        }
    }
}