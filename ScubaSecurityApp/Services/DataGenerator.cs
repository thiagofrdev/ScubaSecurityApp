using ScubaSecurityApp.Models;

namespace ScubaSecurityApp.Services
{
    public static class DataGenerator
    {
        private static readonly Random _random = new Random();

        public static List<Mergulhador> GerarDadosMergulhadores(int qtd)
        {
            var listaDeDados = new List<Mergulhador>();

            for (int id = 1; id <= qtd; id++)
            {
                double profundidade = _random.NextDouble() * 40; //De 0 a 40 metros
                double pressao = 50 + (_random.NextDouble() * 250); //De 50 a 500 Bar
                listaDeDados.Add(new Mergulhador(id, profundidade, pressao));
            }

            return listaDeDados;
        }
    }
}
