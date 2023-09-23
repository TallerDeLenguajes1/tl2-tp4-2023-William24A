//Consultar como hacer una dezerializacion sin necesidad de usar set
namespace WebAPI;
public enum EstadoCadete
{
    EntregandoEncangue,
    Libre
}
public class Cadete
{
    private int id;
    private string nombre;
    private string direccion;
    private int telefono;
    private EstadoCadete estadoCadete;
    private Informe informe;

    public int Id { get => id; }
    public string Nombre { get => nombre; }
    public string Direccion { get => direccion; }
    public int Telefono { get => telefono; }
    public EstadoCadete EstadoCadete {get => estadoCadete;}
    public Informe Informe{ get => informe;}
    public Cadete()
    {
        id = 0;
        telefono = 0;
        informe = new Informe();
        estadoCadete = EstadoCadete.Libre;
        informe = new Informe();
    }

    public Cadete(int id, string nombre, string direccion, int telefono)
    {
        this.id = id;
        this.nombre = nombre;
        this.direccion = direccion;
        this.telefono = telefono;
        estadoCadete = EstadoCadete.Libre;
        informe = new Informe();
    }
     public void CambiarDatos(int id, string nombre, string direccion, int telefono)
    {
        this.id = id;
        this.nombre = nombre;
        this.direccion = direccion;
        this.telefono = telefono;
    }
    public void CambiarEstado()
    {
        if(estadoCadete == EstadoCadete.EntregandoEncangue)
        {
            estadoCadete = EstadoCadete.Libre;
        }
        else
        {
            estadoCadete = EstadoCadete.EntregandoEncangue;
        }
    }
    public string InformeCadete()
    {
        return $"ID: {id}\nNombre cadete: {nombre}\nDireccion: {direccion}\nTelefono: {telefono}\n";
    }
    public Informe RetornarInforme()
    {
        return informe;
    }
}