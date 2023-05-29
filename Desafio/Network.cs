namespace Desafio.Entidades
{
    public class Network
    {
        // Aqui é onde a mágica acontece, esta classe possui a lista de Elementos e seus respectivos métodos.
        // Foi preciso utilizar recursividade para auxiliar o método de query e otimizar a busca.
        public IList<Element> Elements { get; set; }

        // O construtor verifica se os números são positivos e inicializa a classe dependendo o valor informado.
        public Network(int quantidadeElemento)
        {
            if (quantidadeElemento <= 0)
            {
                throw new InvalidOperationException($"Somente números acima de ZERO.");
            }

            Elements = new List<Element>();

            for (int i = 1; i <= quantidadeElemento; i++)
            {
                Elements.Add(new Element(i));
            }
        }

        // Verifica se os elementos existem na "Network" e se já possui a conexão, caso contrário cria a conexão.
        public void Connect(int id1, int id2)
        {
            Element? element1 = Elements.Where(x => x.Id == id1).FirstOrDefault();
            if (element1 == null)
            {
                throw new InvalidOperationException($"Não existe elemento com id {id1}");
            }

            Element? element2 = Elements.Where(x => x.Id == id2).FirstOrDefault();
            if (element2 == null)
            {
                throw new InvalidOperationException($"Não existe elemento com id {id2}");
            }

            if (element1.Connections == null)
            {
                element1.Connections = new List<int>();
            }

            if (element2.Connections == null)
            {
                element2.Connections = new List<int>();
            }

            if (element1.Connections.Contains(id2) || element2.Connections.Contains(id1))
            {
                return;
            }

            element1.Connections.Add(id2);
            element2.Connections.Add(id1);
        }

        // Faz as verificações padrões de elemento, se existe, se possui algum tipo de conexão, 
        // se há, conexão, verifica se é a solicitada e retorna, caso existam multiplas conexões, e a conexão direta não é a solicitada ele faz uma varredura das conexões 
        // dos filhos utilizando de recursividade e uma lista de "visitados" para evitar um loop infinito, desta forma seria como solucionar um laberinto.
        public bool Query(int id1, int id2)
        {
            Element? element1 = Elements.Where(x => x.Id == id1).FirstOrDefault();
            if (element1 == null)
            {
                throw new InvalidOperationException($"Não existe elemento com id {id1}");
            }

            Element? element2 = Elements.Where(x => x.Id == id2).FirstOrDefault();
            if (element2 == null)
            {
                throw new InvalidOperationException($"Não existe elemento com id {id2}");
            }

            if (element1.Connections == null)
            {
                return false;
            }

            if (element2.Connections == null)
            {
                return false;
            }

            if (element1.Connections.Contains(element2.Id) || element2.Connections.Contains(element1.Id))
            {
                return true;
            }

            IList<int> elementosVisitados = new List<int>();

            bool resultado = Recursivo(element1, element2.Id, elementosVisitados);

            return resultado;
        }

        // Metodo privado para auxiliar no metodo Query a encontrar conexões não diretas.
        private bool Recursivo(Element elementoPai, int idParaAchar, IList<int> elementosVisitados)
        {
            if (elementoPai == null)
            {
                throw new ArgumentNullException(nameof(elementoPai));
            }

            if (elementosVisitados == null)
            {
                throw new ArgumentNullException(nameof(elementosVisitados));
            }

            elementosVisitados.Add(elementoPai.Id);

            if (elementoPai.Connections == null)
            {
                return false;
            }

            if (elementoPai.Connections.Contains(idParaAchar))
            {
                return true;
            }

            Element? elementoFilho;
            bool resultado;
            foreach (int elemento in elementoPai.Connections.Where(x => !elementosVisitados.Contains(x)))
            {
                elementoFilho = Elements.Where(x => x.Id == elemento).FirstOrDefault();
                if (elementoFilho == null)
                {
                    return false;
                }

                resultado = Recursivo(elementoFilho, idParaAchar, elementosVisitados);
                if (resultado)
                {
                    return resultado;
                }
            }

            return false;
        }
    }
}
