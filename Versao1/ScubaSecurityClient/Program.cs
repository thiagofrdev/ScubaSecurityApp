using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace ScubaSecurityClient
{
    class Program
    {
        /// <summary>
        /// Ponto de entrada do Cliente. 
        /// Inicia a simulação paralela de múltiplos sensores.
        /// </summary>
        static void Main(string[] args)
        {
            Console.WriteLine("=============================================");
            Console.WriteLine(" SCUBA SECURITY - SENSORES (CLIENTE - V1) ");
            Console.WriteLine("=============================================\n");

            Console.WriteLine("Quantos mergulhadores deseja monitorar?");
            Console.WriteLine("Dica: Aperte ENTER para usar o valor padrão (10)");
            Console.Write("Quantidade: ");

            string input = Console.ReadLine()?.Trim().ToUpper() ?? "";
            int quantidadeMergulhadores = string.IsNullOrWhiteSpace(input) ? 10 : int.Parse(input);

            if (quantidadeMergulhadores == 10)
            {
                Console.WriteLine("Usando valor padrão: 10 mergulhadores.");
            }

            Console.WriteLine($"Iniciando simulação com {quantidadeMergulhadores} Threads (Mergulhadores)...\n");

            // Dispara as Threads, cada uma simulando um mergulhador diferente enviando dados para o servidor.
            for (int i = 1; i <= quantidadeMergulhadores; i++)
            {
                int idMergulhador = i;
                Thread threadSensor = new Thread(() => SimularSensorMergulhador(idMergulhador));
                threadSensor.Start();
            }

            Console.WriteLine("Pressione [ENTER] para encerrar os sensores...\n");
            Console.ReadLine();
        }

        /// <summary>
        /// Simula o sensor de um mergulhador rodando em paralelo.
        /// Complexidade: O(1) por iteração de envio, rodando em loop infinito.
        /// Razão: Cada thread executa operações de tempo constante (geração de randômicos e escrita na stream de rede).
        /// </summary>
        /// <param name="id">Identificador único do mergulhador simulado.</param>
        static void SimularSensorMergulhador(int id)
        {
            try
            {
                using TcpClient cliente = new TcpClient("127.0.0.1", 8080);
                using NetworkStream stream = cliente.GetStream();
                using StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };

                Random rand = new Random();
                double pressaoBar = 200; // Cilindro cheio

                while (pressaoBar > 0)
                {
                    int profundidade = rand.Next(10, 40);
                    double consumo = 0.4 - (rand.NextDouble() * (0.4 - 0.9));
                    pressaoBar -= consumo;

                    string payload = $"ID:{id:D2} | Profundidade:{profundidade}m | Pressao:{pressaoBar:F2}Bar";

                    writer.WriteLine(payload);
                    Console.WriteLine($"[Mergulhador {id:D2}] Dado enviado.");

                    Thread.Sleep(100);
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"[Mergulhador {id:D2}] Falha ao conectar com o barco.");
            }
        }
    }
}