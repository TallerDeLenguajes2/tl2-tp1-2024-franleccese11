
public class Pedido
{
    private int nro;
    private string obs;
    Cliente cliente;
    private estadoPedido estado;

    private Cadete cadete;

    public Pedido(int nro,string obs,Cliente cliente)
    {
        this.nro=nro;
        this.obs = obs;
        this.cliente = cliente;
        estado = estadoPedido.sinCadete;
    }

    public estadoPedido Estado { get => estado; set => estado = value; }
    public int Nro { get => nro;}

    public void AsignarCadete(Cadete cadete)
    {
        this.cadete = cadete;
    }

    public int IdCadete()
    {
        return cadete.Id;
    }

    public string VerDireccionCliente()
    {
        return cliente.Direccion;
    }

    public string VerDatosCliente()
    {
        return cliente.Nombre;
    }
}
