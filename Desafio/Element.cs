namespace Desafio.Entidades
{
    public class Element
    {
        // Cada elemento irá conter um identificador, no caso o ID e uma lista de suas conexões diretas.
        public int Id { get; set; }
        public IList<int>? Connections { get; set; }

        public Element(int id)
        {
            Id = id;
        }
    }
}
