﻿using System.ComponentModel.Design;
using System.Formats.Tar;

List<Cadete> listaCadetes = cargarCadetes("cadetes1.csv");
Cadeteria cadeteria = cargarCadeteria(listaCadetes,"cadeteria1.csv");

int menu=5;
string cadMenu="";
int nro=0;
do
{
    do
    {
        Console.WriteLine("");
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
                cadeteria.listaPedidos.Add(pedido);
                Console.WriteLine("pedido ingresado con exito!");
                    
            break;

            case 1:
                Console.WriteLine("--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*");
                Console.WriteLine("***interfaz de asignacion de pedidos sin cadete***");
                
                
                int interfaz=0;
                string cadInterfaz="";
                do
                {
                    do
                    {
                        Console.WriteLine("---------------------");
                        Console.WriteLine("ingrese 0 para volver atras");
                        Console.WriteLine("ingrese 1 para asignar un pedido a un cadete");
                        cadInterfaz=Console.ReadLine();
                        if (!int.TryParse(cadInterfaz,out interfaz)||interfaz>1||interfaz<0)
                        {
                            Console.WriteLine("ERROR! ingrese un numero valido");
                        }
                    } while (!int.TryParse(cadInterfaz,out interfaz)||interfaz>1||interfaz<0); 
                    if (interfaz==1)
                    {
                        Console.WriteLine("----------------------");
                        foreach (Pedido ped in cadeteria.listaPedidos)
                        {
                            if (ped.Estado==estadoPedido.sinCadete)
                            {
                            Console.WriteLine("numero de pedido:"+ped.Nro);
                                ped.VerDireccionCliente(); 
                            }        
                        }
                        Console.WriteLine("----------------------");

                        if (cadeteria.ControlarPedidosSinCadete()==true)
                        {
                            string cadNro = "";
                            int nroPedido=-1;
                            do
                            {
                                Console.WriteLine("ingrese el numero del pedido:");
                                cadNro = Console.ReadLine();
                            } while (!int.TryParse(cadNro, out nroPedido));
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
                                if (!int.TryParse(cadId,out id) || id<0 || id> listaCadetes.Count)
                                {
                                    Console.WriteLine("ERROR! ingrese un id valido");
                                }
                            } while (!int.TryParse(cadId,out id)|| id<0 || id> listaCadetes.Count);
                            cadeteria.AsignarPedido(id,nroPedido);
                        }else
                        {
                            Console.WriteLine("--no hay ningun pedido que no tenga un cadete asignado!--");
                            interfaz=0;
                        }
               
                    }
                } while (interfaz != 0);
            break;
            case 2:
                Console.WriteLine("/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/");
                Console.WriteLine("***interfaz para cambiar el estado de un pedido***");
                    foreach (Pedido ped in cadeteria.listaPedidos)
                    {
                        Console.WriteLine("-)Pedido numero: "+ped.Nro+", en estado: "+ped.Estado);
                        ped.VerDireccionCliente();
                    }
                    Console.WriteLine("-------------------");
                    string cadNro1 = "";
                    int nroPedido1;
                    do
                    {
                    Console.WriteLine("ingrese el numero del pedido que desea cambiar de estado:");
                    cadNro1 = Console.ReadLine();
                    } while (!int.TryParse(cadNro1, out nroPedido1));
                    string cadInterfaz2="";
                    int interfaz2=-1;
                    do
                    {
                        Console.WriteLine("ingrese 1 para seleccionar el pedido como completado");
                        Console.WriteLine("ingrese 2 para cancelar el pedido");
                        cadInterfaz2=Console.ReadLine();
                    } while (!int.TryParse(cadInterfaz2,out interfaz2)||interfaz2>2||interfaz2<1);
                    cadeteria.CambiarEstadoPedido(nroPedido1,interfaz2);      
            break;

            case 3:
                if (cadeteria.ControlarPedidosPendiente())
                {
                    Console.WriteLine("/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/");
                    Console.WriteLine("***interfaz para reasignar de cadete un pedido***");
                    Console.WriteLine("");
                    foreach (Pedido ped in cadeteria.listaPedidos)
                    {
                        if (ped.Estado != estadoPedido.completado &&ped.Estado!=estadoPedido.sinCadete)
                        {
                           Console.WriteLine("-)Pedido numero: "+ped.Nro+ ", asignado al cadete con ID "+ ped.Cadete.Id);
                            ped.VerDireccionCliente(); 
                        }
                    }
                    string cadNro2="";
                    int nroPedido2;
                    do
                    {
                        Console.WriteLine("ingrese el numero del pedido que desea cambiar de cadete:");
                        cadNro2 = Console.ReadLine();
                    } while (!int.TryParse(cadNro2, out nroPedido2));

                    int id2=0;
                    string cadId2="";
                    Console.WriteLine("ingrese el ID del cadete al que le reasignara el pedido:");
                    foreach (Cadete cadete in cadeteria.listaCadetes)
                    {
                        Console.WriteLine(cadete.Nombre+" con ID: "+cadete.Id);
                    }
                    do
                    {
                        Console.WriteLine("id:");
                        cadId2 = Console.ReadLine();
                        if (!int.TryParse(cadId2,out id2) || id2<0 || id2>listaCadetes.Count)
                        {
                            Console.WriteLine("ERROR! ingrese un id valido");
                        }
                    } while (!int.TryParse(cadId2,out id2)|| id2<0 || id2>listaCadetes.Count);
                    cadeteria.reasignarPedido(id2,nro);
                }else
                {
                    Console.WriteLine("--no hay ningun pedido disponible para reasignar de cadete--");
                }
            break;
        }    
    }
} while (menu!=4);
Console.WriteLine("/*/*/*/*/ INFORME DE LA JORNADA /*/*/*/*/");
int totalEnvios=0;
int totalApagar=0;
foreach (Cadete cadete in listaCadetes)
{
    Console.Write("---cadete "+cadete.Nombre+" con ID: "+cadete.Id);
    Console.WriteLine("cantidad de pedidos realizados: "+cadeteria.PedidosRealizadosJornal(cadete.Id));
    Console.WriteLine("total a pagarle: "+cadeteria.JornalAcobrar(cadete.Id));
    totalEnvios += cadeteria.PedidosRealizadosJornal(cadete.Id);
    totalApagar += cadeteria.JornalAcobrar(cadete.Id);
}
Console.WriteLine("La cantidad de envios realizados con exito en la jornada son: "+ totalEnvios);
Console.WriteLine("el total a pagar a todos los cadetes es: "+totalApagar);




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

/*static void asignarPedido(int id,int nro,List<Pedido>cadeteria.listaPedidos,List<Cadete>listaCadetes)
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
        foreach (Pedido ped in cadeteria.listaPedidos)
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
            cadeteria.listaPedidos.Remove(pedidoAux);
            Console.WriteLine("pedido asignado al cadete con exito!");
        }
    }

}


static void asignarTodosLosPedidos(int id,List<Pedido>cadeteria.listaPedidos,List<Cadete>listaCadetes)
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
        
        for (int i = 0; i < cadeteria.listaPedidos.Count; i++)
        {
            Pedido pedidoAux= cadeteria.listaPedidos[i];
            cadeteAux.listaPedidos.Add(pedidoAux);
            cadeteria.listaPedidos.Remove(pedidoAux);
            i--;
        }
        Console.WriteLine("pedidos transferidos con exito al cadete!");
    }
}*/



