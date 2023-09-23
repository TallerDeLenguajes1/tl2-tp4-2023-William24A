using System.Text.Json;

namespace WebAPI;
public abstract class AccesoADatosCadeteria
{
    public virtual Cadeteria LeerDatosCadeteria(string ruta)
    {
        return null;
    }
    public virtual void CargarDatosCadeterias(Cadeteria cadeteria, string ruta)
    {
    }

}
public class AccesoCadeteriaCSV: AccesoADatosCadeteria
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
}

public class AccesoCadeteriaJSON: AccesoADatosCadeteria
{
    public override Cadeteria LeerDatosCadeteria(string ruta)
    {
        Cadeteria cadeteria = null;

        string pathJSON = Directory.GetCurrentDirectory()+"\\"+ruta;
        string Json = File.ReadAllText(pathJSON);
        cadeteria = JsonSerializer.Deserialize<Cadeteria>(Json);

        return cadeteria;
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
}