public class Cadete
{
    private int id;
    private string nombre;

    private string direccion;
    private string telefono;
    public List<Pedido> listaPedidos;

    public int JornalAcobrar()
    {
        int cantidadJornal = listaPedidos.Count();
        return(500*cantidadJornal);
    }
    
}
