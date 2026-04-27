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

            int quantidadeMergulhadores;

            while (true)
            {
                Console.WriteLine("Quantos mergulhadores deseja monitorar?");
                Console.WriteLine("Dica: Digite 'Q' para usar o valor padrão (10)");
                Console.Write("Quantidade: ");

                string input = Console.ReadLine()?.Trim().ToUpper();

                if (input == "Q" || input == "q")
                {
                    quantidadeMergulhadores = 10;
                    Console.WriteLine("Usando valor padrão: 10 mergulhadores.");
                    break;
                }

                if (int.TryParse(input, out quantidadeMergulhadores) && quantidadeMergulhadores > 0)
                {
                    break;
                }

                Console.WriteLine("\n[ERRO] Entrada inválida! Por favor, digite um número inteiro positivo ou 'Q'.\n");
            }

            Console.WriteLine($"\nIniciando monitoramento de {quantidadeMergulhadores} mergulhadores...\n");

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