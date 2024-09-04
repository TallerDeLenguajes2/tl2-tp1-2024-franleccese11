using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

public class Cadete
{
    
    [JsonPropertyName("id")]
    public int Id { get ; set; }
    
    [JsonPropertyName("nombre")]
    public string Nombre { get; set; }

    [JsonPropertyName("direccion")]
    public string Direccion { get; set; }

    [JsonPropertyName("telefono")]
    public string Telefono { get; set; }

    public Cadete(int id,string nombre,string direccion,string telefono)
    {
        this.Id = id;
        this.Nombre = nombre;
        this.Direccion = direccion;
        this.Telefono=telefono;
    }

   /* public int ContarPedidosRealizados()
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
*/    
}
