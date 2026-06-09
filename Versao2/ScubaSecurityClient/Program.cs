using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using ScubaSecurityClient.Models;
using ScubaSecurityClient.Algorithms;
using ScubaSecurityClient.Security;

namespace ScubaSecurityClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=============================================");
            Console.WriteLine(" SCUBA SECURITY - SENSORES (V2 - Seguro e Otimizado) ");
            Console.WriteLine("=============================================\n");

            try {
                CryptoHelper.InicializarComRuidoAcustico("mergulho.wav");
                Console.WriteLine("[SEGURANÇA] Criptografia de Entropia Acústica ATIVADA.\n");
            } catch (Exception ex) {
                Console.WriteLine($"[ERRO] {ex.Message}"); return;
            }

            Console.WriteLine("Quantos mergulhadores deseja monitorar?");
            Console.WriteLine("Dica: Aperte ENTER para usar o valor padrão (10)");
            Console.Write("Quantidade: ");

            string input = Console.ReadLine()?.Trim().ToUpper() ?? "";
            int quantidadeMergulhadores = string.IsNullOrWhiteSpace(input) ? 10 : int.Parse(input);

            if (quantidadeMergulhadores == 10)
            {
                Console.WriteLine("Usando valor padrão: 10 mergulhadores.");
            }
            
            for (int i = 1; i <= quantidadeMergulhadores; i++)
            {
                int idMergulhador = i;
                Thread threadSensor = new Thread(() => SimularSensorMergulhador(idMergulhador));
                threadSensor.Start();
            }
            Console.ReadLine();
        }

        static void SimularSensorMergulhador(int id)
        {
            try
            {
                using TcpClient cliente = new TcpClient("127.0.0.1", 8080);
                using NetworkStream stream = cliente.GetStream();
                using StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };
                using StreamReader reader = new StreamReader(stream);

                Random rand = new Random();
                double pressaoBar = 200.0; 
                double ultimaPressaoEnviada = 200.0;

                while (pressaoBar > 0)
                {
                    double profundidade = rand.Next(10, 40);
                    double consumo = 0.5 + (rand.NextDouble() * (1.3 - 0.5));
                    pressaoBar -= consumo;

                    if (ultimaPressaoEnviada - pressaoBar >= 5.0)
                    {
                        string payload = $"ID:{id:D2} | Profundidade:{profundidade}m | Pressao:{pressaoBar:F2}Bar";
                        
                        string payloadCifrado = CryptoHelper.Encriptar(payload);
                        if (id == 1)
                        {
                            Console.WriteLine($"\n[Merg 01 - Gerado] {payload}");
                            Console.WriteLine($"[Merg 01 - Cifrado via Áudio] {payloadCifrado}");
                        }

                        writer.WriteLine(payloadCifrado);
                        ultimaPressaoEnviada = pressaoBar;

                        string? respostaCifrada = reader.ReadLine();
                        if (respostaCifrada != null)
                        {
                            string respostaPlana = CryptoHelper.Decriptar(respostaCifrada);
                            List<Mergulhador> listaGlobal = ParseListaGlobal(respostaPlana);

                            if (id == 1)
                            {
                                Console.WriteLine($"\n--- PROCESSAMENTO LOCAL (CLIENTE 01) ---");
                                Sorters.BubbleSortByPressao(listaGlobal);
                                Analysis.AnalisarAutonomiaCruzada(listaGlobal);
                                Console.WriteLine($"----------------------------------------\n");
                            }
                        }
                    }
                    Thread.Sleep(100); 
                }
            }
            catch { /* Silencia falhas de conexão no loop */ }
        }

        // Método auxiliar para reconstruir a lista de objetos a partir da string recebida
        static List<Mergulhador> ParseListaGlobal(string dados)
        {
            List<Mergulhador> lista = new List<Mergulhador>();
            string[] mergulhadores = dados.Split(';', StringSplitOptions.RemoveEmptyEntries);
            foreach (var m in mergulhadores)
            {
                string[] props = m.Split(',');
                lista.Add(new Mergulhador(int.Parse(props[0]), double.Parse(props[1]), double.Parse(props[2])));
            }
            return lista;
        }
    }
}