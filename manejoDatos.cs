using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

public abstract class AccesoADatos
{
    public abstract List<Cadete> CargarCadetes(string nombreArchivo);
    public abstract Cadeteria CargarCadeteria(List<Cadete> listaCadetes,string nombreArchivo2);
}

public class ArchivoCsv : AccesoADatos
{
    public override List<Cadete> CargarCadetes(string nombreArchivo)
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

    public override Cadeteria CargarCadeteria(List<Cadete> listaCadetes,string nombreArchivo2)
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
}


public class ArchivoJson : AccesoADatos

{

     public class CadeteriaDeserialize
    {
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; }

        [JsonPropertyName("telefono")]
        public int Telefono { get; set; }
    }


    
    public override List<Cadete> CargarCadetes(string nombreArchivo)
    {
        List<Cadete> listaDeCadetes;
        using (StreamReader strReader = new StreamReader(nombreArchivo))
        {
            string listaCadetesJson = strReader.ReadToEnd();
            listaDeCadetes = JsonSerializer.Deserialize<List<Cadete>>(listaCadetesJson);
        }
        return listaDeCadetes; 
    }

    public override Cadeteria CargarCadeteria(List<Cadete> listaCadetes, string nombreArchivo2)
    {
        CadeteriaDeserialize cadeteriaDeserialize;
        using (StreamReader strReader = new StreamReader(nombreArchivo2))
        {
            string cadeteriaJSON = strReader.ReadToEnd();
            cadeteriaDeserialize = JsonSerializer.Deserialize<CadeteriaDeserialize>(cadeteriaJSON);
        }
        Cadeteria cadeteria = new Cadeteria(cadeteriaDeserialize.Nombre,cadeteriaDeserialize.Telefono,listaCadetes);
        return cadeteria;
    }
}

