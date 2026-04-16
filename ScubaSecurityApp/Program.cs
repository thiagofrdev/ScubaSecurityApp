using System;
using ScubaSecurityApp.Models;
using ScubaSecurityApp.Services;

namespace ScubaSecurityApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=============================================");
            Console.WriteLine(" SCUBA SECURITY - MONITORAMENTO DE MERGULHO ");
            Console.WriteLine("=============================================\n");

            int quantidadeMergulhadores = 10;
            List<Mergulhador> mergulhadores = DataGenerator.GerarDadosMergulhadores(quantidadeMergulhadores);

            Console.WriteLine("=== Lista de Mergulhadores Monitorados ===");
            foreach (var m in mergulhadores)
            {
                Console.WriteLine(m.ToString());
            }

            Console.WriteLine("\n---------------------------------------------");
            Console.WriteLine("Pressione qualquer tecla para encerrar...");
            Console.ReadKey();
        }
    }
}