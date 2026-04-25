using System;
using ScubaSecurityApp.Models;
using ScubaSecurityApp.Services;

namespace ScubaSecurityApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Sistema de Monitoramento de Mergulho ===");
            Console.WriteLine("=============================================");
            Console.WriteLine(" SCUBA SECURITY - MONITORAMENTO DE MERGULHO ");
            Console.WriteLine("=============================================\n");

            int quantidadeMergulhadores = 10;
            List<Mergulhador> mergulhadores = DataGenerator.GerarDadosMergulhadores(quantidadeMergulhadores);

            Console.WriteLine("=== Lista de Mergulhadores Monitorados ===");
            // Complexidade: O(N) - Percorre a lista inteira uma única vez.
            foreach (var m in mergulhadores) Console.WriteLine(m.ToString());

            Console.WriteLine("\n---------------------------------------------");
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();

            ScubaSecurityApp.Algorithms.Sorters.BubbleSortByPressao(mergulhadores);

            Console.WriteLine("\n=== Lista Ordenada por Pressão (Crescente) ===");
            // Complexidade: O(N) - Percorre a lista inteira uma única vez.
            foreach (var m in mergulhadores) Console.WriteLine(m.ToString());

            Console.WriteLine("\n---------------------------------------------");
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();

            Console.WriteLine("\n=== Análise de Segurança: Compatibilidade para compartilhamento de ar ===");
            ScubaSecurityApp.Algorithms.Analysis.AnalisarAutonomiaCruzada(mergulhadores);

            Console.WriteLine("\n---------------------------------------------");
            Console.WriteLine("Pressione qualquer tecla para encerrar...");
            Console.ReadKey();
        }
    }
}