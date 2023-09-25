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

    public int Id { get => id; set => id = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Direccion { get => direccion; set => direccion = value; }
    public int Telefono { get => telefono; set => telefono = value; }
    public EstadoCadete EstadoCadete { get => estadoCadete; set => estadoCadete = value; }
    public Informe Informe { get => informe; set => informe = value; }

    public Cadete()
    {
        Id = 0;
        Telefono = 0;
        Informe = new Informe();
        EstadoCadete = EstadoCadete.Libre;
        Informe = new Informe();
    }

    public Cadete(int id, string nombre, string direccion, int telefono)
    {
        this.Id = id;
        this.Nombre = nombre;
        this.Direccion = direccion;
        this.Telefono = telefono;
        EstadoCadete = EstadoCadete.Libre;
        Informe = new Informe();
    }

    public void CambiarDatos(int id, string nombre, string direccion, int telefono)
    {
        this.Id = id;
        this.Nombre = nombre;
        this.Direccion = direccion;
        this.Telefono = telefono;
    }
    public void CambiarEstado()
    {
        if(EstadoCadete == EstadoCadete.EntregandoEncangue)
        {
            EstadoCadete = EstadoCadete.Libre;
        }
        else
        {
            EstadoCadete = EstadoCadete.EntregandoEncangue;
        }
    }
    public string InformeCadete()
    {
        return $"ID: {Id}\nNombre cadete: {Nombre}\nDireccion: {Direccion}\nTelefono: {Telefono}\n";
    }
    public Informe RetornarInforme()
    {
        return Informe;
    }
}