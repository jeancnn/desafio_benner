using Desafio.Entidades;


namespace Desafio
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Inicializando a classe utilizando 8 elementos o que ajuda a demonstrar o desafio pois é a mesma quantidade utilzada como ex.
            Network network = new Network(8);

            network.Connect(1, 2);
            network.Connect(6, 2);
            network.Connect(2, 4);
            network.Connect(5, 8);
            network.Connect(4, 7); // Aqui foi adicionado um teste extra para ver o sistema ir mais a fundo na busca

            Console.WriteLine($"Elemento 1 e 3 possui conexão: {network.Query(1, 3)}");
            Console.WriteLine($"Elemento 1 e 7 possui conexão: {network.Query(1, 7)}");
            Console.WriteLine($"Elemento 6 e 8 possui conexão: {network.Query(6, 8)}");
            Console.WriteLine($"Elemento 5 e 8 possui conexão: {network.Query(5, 8)}");
        }
    }
}