using System.ComponentModel.Design;

List<Cadete> listaCadetes = cargarCadetes("cadetes1.csv");
Cadeteria cadeteria = cargarCadeteria(listaCadetes,"cadeteria1.csv");
List<Pedido> listaPedidosTotal = new List<Pedido>();

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

    if (menu!=5)
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
                listaPedidosTotal.Add(pedido);
                    
            break;

            case 1:
                Console.WriteLine("***interfaz de asignacion de pedidos sin cadete***");
                IEnumerable<Pedido> listaPedidosSinCadete = from ped in listaPedidosTotal
                                        where ped.Estado == estadoPedido.sinCadete
                                        select ped;
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
                        Console.WriteLine("ingrese 0 para volver atras");
                        Console.WriteLine("ingrese 1 para asignar un solo pedido a un cadete");
                        Console.WriteLine("ingrese 2 para asignar todos los pedidos a un mismo cadete");
                        cadInterfaz=Console.ReadLine();
                        if (!int.TryParse(cadInterfaz,out interfaz)||interfaz>2||interfaz<0)
                        {
                            Console.WriteLine("ERROR! ingrese un numero valido");
                        }else
                        {
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
                                                    
                            }
                        }
                    } while (!int.TryParse(cadInterfaz,out interfaz));
                    
                } while (interfaz != 0);
            break;
            
        }    
    }
} while (menu!=5);



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