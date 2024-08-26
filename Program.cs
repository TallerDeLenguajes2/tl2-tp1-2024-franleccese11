List<Cadete> listaCadetes = cargarCadetes("cadetes1.csv");
Cadeteria cadeteria = cargarCadeteria(listaCadetes,"cadeteria1.csv");





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