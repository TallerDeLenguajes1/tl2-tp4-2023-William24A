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

public class AccesoCadeteriaJSON: AccesoADatosCadeteria
{
    public override Cadeteria LeerDatosCadeteria(string ruta)
    {

        string pathJSON = Directory.GetCurrentDirectory()+"\\"+ruta;
        string Json = File.ReadAllText(pathJSON);
        return JsonSerializer.Deserialize<Cadeteria>(Json);

        
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