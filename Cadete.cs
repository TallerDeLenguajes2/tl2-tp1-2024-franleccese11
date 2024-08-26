public class Cadete
{
    private int id;
    private string nombre;

    private string direccion;
    private string telefono;
    public List<Pedido> listaPedidos;

    public Cadete(int id,string nombre,string direccion,string telefono)
    {
        this.id = id;
        this.nombre = nombre;
        this.direccion = direccion;
        this.telefono=telefono;
        listaPedidos = new List<Pedido>();
    }

    public int ContarPedidosRealizados()
    {
        int entregados=0;
        foreach (Pedido pedido in listaPedidos)
        {
            if (pedido.Estado == estadoPedido.completado)
            {
                entregados++;
            }
        }
        return(entregados);
    }

    public int JornalAcobrar()
    {
        int cantidadJornal = ContarPedidosRealizados();
        return(500*cantidadJornal);
    }
    
}
