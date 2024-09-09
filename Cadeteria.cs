public class Cadeteria
{
    private string nombre;
    private int telefono;
    public List<Cadete> listaCadetes;

    public List<Pedido> listaPedidos;

    public string Nombre { get => nombre;}

    public Cadeteria(string nombre,int telefono,List<Cadete>listaCadetes)
    {
        this.nombre = nombre;
        this.telefono = telefono;
        this.listaCadetes = listaCadetes;
        listaPedidos = new List<Pedido>();
    }

    public int PedidosRealizadosJornal(int id)
    {
        int pedidosRealizados=0;
        foreach (Pedido pedido in listaPedidos)
        {
            if(id==pedido.Cadete.Id && pedido.Estado==estadoPedido.completado)
            {
                pedidosRealizados++;
            }
        }
        return pedidosRealizados;
    }

    public int JornalAcobrar(int id)
    {
        
         
        return 500*PedidosRealizadosJornal(id);
    }
    
    /* public int TotalAPagarJornal()
    {
        int total=0;
        foreach (Cadete item in listaCadetes)
        {
            total = total + item.JornalAcobrar();
        }
        return total;
    }

    public int CantidadEnviostotal()
    {
        int cantEnvios=0;
        foreach (Cadete cadete in listaCadetes)
        {
            foreach (Pedido pedido in cadete.listaPedidos)
            {
                if (pedido.Estado==estadoPedido.completado)
                {
                    cantEnvios++;
                }
            }
        }
        return(cantEnvios);
    }
    */

    public bool AsignarPedido(int id,int nro)
    {
        Cadete cadeteAux= listaCadetes.Find(cad=>cad.Id==id);
        Pedido pedidoAux= listaPedidos.Find(ped=>ped.Nro==nro);
        if (cadeteAux!=null && pedidoAux != null)
        {
            pedidoAux.Estado=estadoPedido.Pendiente;
            pedidoAux.Cadete = cadeteAux;
            return true;
        }else
        {
            return false;
        }
    }


    public bool CambiarEstadoPedido(int nro,int seleccion)
    {
        Pedido pedidoAux = listaPedidos.Find(ped=>ped.Nro==nro);
        if (pedidoAux!=null)
        {
            if (seleccion==1)
                {
                    pedidoAux.Estado=estadoPedido.completado;
                }else
                {
                    listaPedidos.Remove(pedidoAux);
                }
            return true;
        }else
        {
            return false;
        }            
    }


    public bool ReasignarPedido(int id1,int nro)
    {
        Cadete cadeteAux= listaCadetes.Find(cad=>cad.Id==id1);
        Pedido pedidoAux= listaPedidos.Find(ped=>ped.Nro==nro);
        

        if (cadeteAux!=null && pedidoAux!=null)
        {
            pedidoAux.Cadete = cadeteAux;
            return true;
        }else
        {
            return false;
        }
    }

    public bool ControlarPedidosSinCadete()
    {
        foreach (Pedido pedido in listaPedidos)
        {
            if (pedido.Estado==estadoPedido.sinCadete)
            {
                return true;
            }
        }
        return false;
    }

    public bool ControlarPedidosPendiente()
    {
        foreach (Pedido pedido in listaPedidos)
        {
            if (pedido.Estado==estadoPedido.Pendiente)
            {
                return true;
            }
        }
        return false;
    }


}