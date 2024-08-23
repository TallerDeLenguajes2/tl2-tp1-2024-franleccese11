public class Pedido
{
    private int nro;
    private string obs;
    Cliente cliente;
    char estado;

    public void VerDireccionCliente()
    {
        Console.WriteLine("La direccion del cliente es " + cliente.Direccion);
    }

    public void VerDatosCliente()
    {
        Console.WriteLine("Nombre del cliente:"+cliente.Nombre);
        Console.WriteLine("Telefono:"+cliente.Telefono);
    }
}
