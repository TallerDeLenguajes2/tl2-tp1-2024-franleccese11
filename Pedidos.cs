
public class Pedido
{
    private int nro;
    private string obs;
    Cliente cliente;
    private estadoPedido estado;

    public Pedido(int nro,string obs,Cliente cliente)
    {
        this.nro=nro;
        this.obs = obs;
        this.cliente = cliente;
        estado = estadoPedido.sinCadete;
    }

    public estadoPedido Estado { get => estado; set => estado = value; }
    public int Nro { get => nro;}

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
