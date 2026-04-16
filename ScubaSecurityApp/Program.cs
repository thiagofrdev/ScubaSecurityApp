using System;
using ScubaSecurityApp.Models;

namespace ScubaSecurityApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=============================================");
            Console.WriteLine(" SCUBA SECURITY - MONITORAMENTO DE MERGULHO ");
            Console.WriteLine("=============================================\n");

            Mergulhador teste = new Mergulhador(1, 12.5, 195.0);
            Console.WriteLine(teste.ToString());

            Console.WriteLine("\n---------------------------------------------");
            Console.WriteLine("Pressione qualquer tecla para encerrar...");
            Console.ReadKey();
        }
    }
}