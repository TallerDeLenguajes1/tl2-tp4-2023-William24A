using System.Text.Json;

namespace WebAPI;
public abstract class AccesoADatosCadete
{
    public virtual List<Cadete> LeerDatosCadetes(string ruta)
    {
        return null;
    }
    public virtual void CargarDatosCadetes(Cadeteria cadeteria, string ruta)
    {
    }

}

public class AccesoCadeteCSV: AccesoADatosCadete
{
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

}

public class AccesoCadeteJSON: AccesoADatosCadete
{
    public override List<Cadete> LeerDatosCadetes(string ruta)
    {
        List<Cadete> listacadetes = new List<Cadete>();
        string pathJSON = Directory.GetCurrentDirectory()+"\\"+ruta;
        string Json = File.ReadAllText(pathJSON); //Leer archivo y guardar
        listacadetes = JsonSerializer.Deserialize<List<Cadete>>(Json); // aclaracion de lista
        return listacadetes;
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
}