using ScubaSecurityApp.Models;

namespace ScubaSecurityApp.Services
{
    public static class DataGenerator
    {
        private static readonly Random _random = new Random();

        /// <summary>
        /// Gera dados randômicos para N mergulhadores.
        /// 
        /// Complexidade: O(N)
        /// Razão: O algoritmo utiliza um único laço 'for' que itera 'qtd' vezes para instanciar os objetos.
        /// 
        /// Consequências: O tempo de execução cresce de forma linear. Para volumes gigantescos de dados, 
        /// o impacto no desempenho é previsível e diretamente proporcional ao número de entradas.
        /// </summary>
        public static List<Mergulhador> GerarDadosMergulhadores(int qtd)
        {
            var listaDeDados = new List<Mergulhador>();

            for (int id = 1; id <= qtd; id++)
            {
                double profundidade = _random.NextDouble() * 40; //De 0 a 40 metros
                double pressao = 50 + (_random.NextDouble() * 250); //De 50 a 300 Bar
                listaDeDados.Add(new Mergulhador(id, profundidade, pressao));
            }

            return listaDeDados;
        }
    }
}
