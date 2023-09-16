namespace ClienteUtilizar;
public class Cliente
{
    private string nombreCliente;
    private string direccion;
    private int telefono;
    private string? datosreferencia;

    public string NombreCliente { get => nombreCliente; }
    public string Direccion { get => direccion;}
    public int Telefono { get => telefono;}
    public string? Datosreferencia { get => datosreferencia;}

    public Cliente()
    {

    }
    public Cliente(string nombreCliente, string direccion, int telefono, string datosreferencia)
    {
        this.nombreCliente = nombreCliente;
        this.direccion = direccion;
        this.telefono = telefono;
        this.datosreferencia = datosreferencia;
    }
    public void CambiarDatos(string nombreCliente, string direccion, int telefono, string datosreferencia)
    {
        this.nombreCliente = nombreCliente;
        this.direccion = direccion;
        this.telefono = telefono;
        this.datosreferencia = datosreferencia;
    }
    public string Informe()
    {
        return $"Nombre cliente: {nombreCliente}\nDireccion: {direccion}\nTelefono: {telefono}\nDatos referencia: {datosreferencia}\n";
    }
}