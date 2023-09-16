namespace CadeteUtilizar;
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

    public int Id { get => id; }
    public string Nombre { get => nombre; }
    public string Direccion { get => direccion; }
    public int Telefono { get => telefono; }
    public EstadoCadete EstadoCadete {get => estadoCadete;}
    public Cadete()
    {
    }

    public Cadete(int id, string nombre, string direccion, int telefono)
    {
        this.id = id;
        this.nombre = nombre;
        this.direccion = direccion;
        this.telefono = telefono;
        this.estadoCadete = EstadoCadete.Libre;
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
    public string Informe()
    {
        return $"ID: {id}\nNombre cadete: {nombre}\nDireccion: {direccion}\nTelefono: {telefono}\n";
    }
}