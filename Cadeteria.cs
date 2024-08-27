public class Cadeteria
{
    private string nombre;
    private int telefono;
    public List<Cadete> listaCadetes;

    public string Nombre { get => nombre;}

    public Cadeteria(string nombre,int telefono,List<Cadete>listaCadetes)
    {
        this.nombre = nombre;
        this.telefono = telefono;
        this.listaCadetes = listaCadetes;
    }

    public int TotalAPagarJornal()
    {
        int total=0;
        foreach (Cadete item in listaCadetes)
        {
            total = total + item.JornalAcobrar();
        }
        return total;
    }
}