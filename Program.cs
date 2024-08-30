using System.ComponentModel.Design;
using System.Formats.Tar;

List<Cadete> listaCadetes = cargarCadetes("cadetes1.csv");
Cadeteria cadeteria = cargarCadeteria(listaCadetes,"cadeteria1.csv");
List<Pedido> listaPedidosSinCadete = new List<Pedido>();

int menu=5;
string cadMenu="";
int nro=0;
do
{
    do
    {
        Console.WriteLine("*/*/*/ Menu de gestion de pedidos */*/*/ ");
        Console.WriteLine("ingrese 0 para dar de alta un pedido");
        Console.WriteLine("ingrese 1 para asignar pedidos a un cadete");
        Console.WriteLine("ingrese 2 para cambiar de estado un pedido");
        Console.WriteLine("ingrese 3 para reasignar un pedido a otro cadete");
        Console.WriteLine("ingrese 4 para cerrar caja");
        cadMenu=Console.ReadLine();
        if (!int.TryParse(cadMenu,out menu)||menu<0||menu>4)
        {
            Console.WriteLine("ERROR! numero ingresado no valido, por favor ingrese un numero valido");
        }
    } while (!int.TryParse(cadMenu,out menu)||menu<0||menu>4);

    if (menu!=4)
    {
        switch (menu)
        {
            case 0:
                Console.WriteLine("---------Nuevo Pedido---------");
                Console.WriteLine("**Ingrese los datos del cliente**");
                Console.WriteLine("Nombre:");
                string nombre = Console.ReadLine();
                Console.WriteLine("Direccion:");
                string direccion = Console.ReadLine();
                Console.WriteLine("Telefono:");
                string telefono=Console.ReadLine();
                Console.WriteLine("Datos de referencia de la direccion:");
                string datosref = Console.ReadLine();
                Cliente cliente = new(nombre,direccion,telefono,datosref);
                nro++;
                Console.WriteLine("observacion del pedido:");
                string obs=Console.ReadLine();
                Pedido pedido = new(nro,obs,cliente);
                listaPedidosSinCadete.Add(pedido);
                    
            break;

            case 1:
                Console.WriteLine("--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*");
                Console.WriteLine("***interfaz de asignacion de pedidos sin cadete***");
                foreach (Pedido pedidoSinCadete in listaPedidosSinCadete)
                {
                    Console.WriteLine("numero de pedido:"+pedidoSinCadete.Nro);
                    pedidoSinCadete.VerDireccionCliente();
                }
                int interfaz=0;
                string cadInterfaz="";
                do
                {
                    do
                    {
                        Console.WriteLine("---------------------");
                        Console.WriteLine("ingrese 0 para volver atras");
                        Console.WriteLine("ingrese 1 para asignar un solo pedido a un cadete");
                        Console.WriteLine("ingrese 2 para asignar todos los pedidos a un mismo cadete");
                        cadInterfaz=Console.ReadLine();
                        if (!int.TryParse(cadInterfaz,out interfaz)||interfaz>2||interfaz<0)
                        {
                            Console.WriteLine("ERROR! ingrese un numero valido");
                        }
                    } while (!int.TryParse(cadInterfaz,out interfaz)); 
                    if (interfaz==1)
                    {
                        string cadNro = "";
                        do
                        {
                            Console.WriteLine("ingrese el numero del pedido:");
                            cadNro = Console.ReadLine();
                        } while (!int.TryParse(cadNro, out int nroPedido));
                        Console.WriteLine("ingrese el id del cadete al que asignara el pedido:");
                        foreach (Cadete cad in listaCadetes)
                        {
                            Console.WriteLine("-)id:"+cad.Id+" Nombre:"+cad.Nombre);
                        }
                        int id=0;
                        string cadId="";
                        do
                        {
                            Console.WriteLine("id:");
                            cadId = Console.ReadLine();
                            if (!int.TryParse(cadId,out id) || id<0 || id>= listaCadetes.Count)
                            {
                                Console.WriteLine("ERROR! ingrese un id valido");
                            }
                        } while (!int.TryParse(cadId,out id));
                        asignarPedido(id,nro,listaPedidosSinCadete,listaCadetes);            
                    }else
                    {
                        if (interfaz ==2) 
                        {
                            Console.WriteLine("ingrese el id del cadete al que asignara los pedidos:");
                            foreach (Cadete cad in listaCadetes)
                            {
                            Console.WriteLine("-)id:"+cad.Id+" Nombre:"+cad.Nombre);
                            }
                            int id=0;
                            string cadId="";
                            do
                            {
                                Console.WriteLine("id:");
                                cadId = Console.ReadLine();
                                if (!int.TryParse(cadId,out id) || id<0 || id>= listaCadetes.Count)
                                {
                                    Console.WriteLine("ERROR! ingrese un id valido");
                                }
                            }while (!int.TryParse(cadId,out id));
                            asignarTodosLosPedidos(id,listaPedidosSinCadete,listaCadetes);
                        }
                    }
                } while (interfaz != 0);
            break;
            case 2:
                Console.WriteLine("--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*");
                Console.WriteLine("***interfaz para cambiar el estado de un pedido***");
                foreach (Cadete cadete in listaCadetes)
                {
                    foreach (Pedido ped in cadete.listaPedidos)
                    {
                        Console.WriteLine("-)Pedido numero: "+ped.Nro);
                        ped.VerDireccionCliente();
                    }
                }
                string cadNro1 = "";
                int nroPedido1;
                do
                {
                    Console.WriteLine("ingrese el numero del pedido que desea cambiar de estado:");
                    cadNro1 = Console.ReadLine();
                } while (!int.TryParse(cadNro1, out nroPedido1));
                cambiarEstadoPedido(listaCadetes,nroPedido1);
            break;

            case 3:
                Console.WriteLine("***interfaz para reasignar de cadete un pedido***");
                foreach (Cadete cadete in listaCadetes)
                {
                    Console.WriteLine("---Pedidos del cadete "+cadete.Nombre+" con ID:"+cadete.Id+"---");
                    foreach (Pedido ped in cadete.listaPedidos)
                    {
                        if (ped.Estado ==estadoPedido.Pendiente)
                        {
                           Console.WriteLine("-)Pedido numero: "+ped.Nro);
                        ped.VerDireccionCliente(); 
                        }
                    }
                }
                int id1=0;
                string cadId1="";
                Console.WriteLine("ingrese el ID del cadete al que le quitara el pedido");
                do
                    {
                        Console.WriteLine("id:");
                        cadId1 = Console.ReadLine();
                        if (!int.TryParse(cadId1,out id1) || id1<0 || id1> listaCadetes.Count)
                        {
                            Console.WriteLine("ERROR! ingrese un id valido");
                        }
                        } while (!int.TryParse(cadId1,out id1));
                string cadNro2 = "";
                int nroPedido2;
                do
                {
                    Console.WriteLine("ingrese el numero del pedido que desea cambiar de cadete:");
                    cadNro2 = Console.ReadLine();
                } while (!int.TryParse(cadNro2, out nroPedido2));
                int id2=0;
                string cadId2="";
                Console.WriteLine("ingrese el ID del cadete al que le reasignara el pedido");
                do
                    {
                        Console.WriteLine("id:");
                        cadId2 = Console.ReadLine();
                        if (!int.TryParse(cadId2,out id2) || id2<0 || id2>listaCadetes.Count)
                        {
                            Console.WriteLine("ERROR! ingrese un id valido");
                        }
                        } while (!int.TryParse(cadId2,out id2));
                reasignarPedido(id1,id2,nroPedido2,listaCadetes);
            break;
        }    
    }
} while (menu!=4);
Console.WriteLine("/*/*/*/*/ INFORME DE LA JORNADA /*/*/*/*/");
foreach (Cadete cadete in listaCadetes)
{
    Console.Write("---cadete "+cadete.Nombre+" con ID: "+cadete.Id);
    Console.WriteLine("cantidad de pedidos realizados: "+cadete.ContarPedidosRealizados());
    Console.WriteLine("total a pagarle: "+cadete.JornalAcobrar());
}
Console.WriteLine("La cantidad de envios realizados con exito en la jornada son: "+cadeteria.CantidadEnviostotal());
Console.WriteLine("el total a pagar a todos los cadetes es: "+cadeteria.TotalAPagarJornal());




static List<Cadete> cargarCadetes(string nombreArchivo)
{
    List<Cadete> listaDeCadetes = new List<Cadete>();
    using(StreamReader str = new StreamReader(nombreArchivo))
    {
        str.ReadLine();
        string linea="";
        while (!str.EndOfStream)
        {
            linea=str.ReadLine();
            string []valores= linea.Split(',');
            int id = int.Parse(valores[0]);
            Cadete cadete = new Cadete(id,valores[1],valores[2],valores[3]);
            listaDeCadetes.Add(cadete);
        }
    }
    return listaDeCadetes;
} 

static Cadeteria cargarCadeteria(List<Cadete> listaCadetes,string nombreArchivo2)
{
    Cadeteria cadeteria;
    using(StreamReader str = new StreamReader(nombreArchivo2))
    {
        str.ReadLine();
        string linea=str.ReadLine();
        string []valores= linea.Split(',');
        cadeteria = new Cadeteria(valores[0],int.Parse(valores[1]),listaCadetes);
    }
    return cadeteria;
}

static void asignarPedido(int id,int nro,List<Pedido>listaPedidosSinCadete,List<Cadete>listaCadetes)
{
    Cadete cadeteAux=null;
    foreach (Cadete cad in listaCadetes)
    {
        if (id==cad.Id)
        {
            cadeteAux=cad;
            break;
        }
    }
    if (cadeteAux!=null)
    {
        Pedido pedidoAux=null;
        foreach (Pedido ped in listaPedidosSinCadete)
        {
            if (nro==ped.Nro)
            {
                pedidoAux=ped;
                break;
            }
        }
        if (pedidoAux!=null)
        {
            pedidoAux.Estado=estadoPedido.Pendiente;
            cadeteAux.listaPedidos.Add(pedidoAux);
            listaPedidosSinCadete.Remove(pedidoAux);
            Console.WriteLine("pedido asignado al cadete con exito!");
        }
    }

}


static void asignarTodosLosPedidos(int id,List<Pedido>listaPedidosSinCadete,List<Cadete>listaCadetes)
{
    Cadete cadeteAux=null;
    foreach (Cadete cad in listaCadetes)
    {
        if (id==cad.Id)
        {
            cadeteAux=cad;
            break;
        }
    }
    if (cadeteAux!=null)
    {
        
        for (int i = 0; i < listaPedidosSinCadete.Count; i++)
        {
            Pedido pedidoAux= listaPedidosSinCadete[i];
            cadeteAux.listaPedidos.Add(pedidoAux);
            listaPedidosSinCadete.Remove(pedidoAux);
            i--;
        }
        Console.WriteLine("pedidos transferidos con exito al cadete!");
    }
}

static void cambiarEstadoPedido(List<Cadete>listaCadetes,int nro)
{
    foreach (Cadete cadete in listaCadetes)
    {
        foreach (Pedido pedido in cadete.listaPedidos)
        {
            if (nro == pedido.Nro)
            {
                pedido.Estado=estadoPedido.completado;
                Console.WriteLine("pedido cambiado de estado con exito!");
            }
        }
    }
}

static void reasignarPedido(int id1,int id2,int nro,List<Cadete>listaDeCadetes)
{
    Cadete cadeteAux=null;
    Cadete cadeteAux2=null;
    foreach (Cadete cadete in listaDeCadetes)
    {
       if (id1==cadete.Id)
       {
        cadeteAux=cadete;
       }
       if (id2==cadete.Id)
       {
        cadeteAux2=cadete;
       }
    }

    if (cadeteAux!=null && cadeteAux2!=null)
    {
        Pedido pedidoAux =null;
        foreach (Pedido pedido in cadeteAux.listaPedidos)
        {
            if (nro==pedido.Nro)
            {
                pedidoAux=pedido;
                break;
            }
        }
        if (pedidoAux!=null)
        {
            cadeteAux2.listaPedidos.Add(pedidoAux);
            cadeteAux.listaPedidos.Remove(pedidoAux);
        }
    }
}