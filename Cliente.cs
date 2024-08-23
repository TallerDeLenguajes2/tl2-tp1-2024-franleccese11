public class Cliente
{
    private string nombre;
    private string direccion;
    private int telefono;
    private string datosReferenciaDireccion;

    public string Nombre { get => nombre;}
    public string Direccion { get => Direccion;}
    public int Telefono { get => telefono; set => telefono = value; }
    public string DatosReferenciaDireccion{ get => datosReferenciaDireccion;}
}