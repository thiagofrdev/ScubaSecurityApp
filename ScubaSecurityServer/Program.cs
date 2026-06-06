using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Threading;
using ScubaSecurityServer.Models;
using ScubaSecurityServer.Algorithms;

namespace ScubaSecurityServer
{
    class Program
    {
        static List<Mergulhador> estadoMergulhadores = new List<Mergulhador>();
        static readonly object lockObj = new object();

        static async Task Main(string[] args)
        {
            Console.WriteLine("=============================================");
            Console.WriteLine(" SCUBA SECURITY - SERVIDOR (Versão 1 - Sem Otimização) ");
            Console.WriteLine("=============================================\n");

            int porta = 8080;
            TcpListener listener = new TcpListener(IPAddress.Any, porta);

            try
            {
                listener.Start();
                Console.WriteLine($"[SERVIDOR] A escutar na porta {porta}...\n");

                Thread threadProcessamento = new Thread(ProcessarDadosPesados);
                threadProcessamento.Start();

                while (true)
                {
                    TcpClient cliente = await listener.AcceptTcpClientAsync();
                    _ = Task.Run(() => TratarCliente(cliente));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERRO SERVIDOR] {ex.Message}");
            }
        }

        static void TratarCliente(TcpClient cliente)
        {
            try
            {
                using NetworkStream stream = cliente.GetStream();
                using StreamReader reader = new StreamReader(stream);

                while (cliente.Connected)
                {
                    string? mensagem = reader.ReadLine();
                    if (mensagem == null) break; 

                    try 
                    {
                        string[] partes = mensagem.Split('|');
                        int id = int.Parse(partes[0].Split(':')[1].Trim());
                        int profundidade = int.Parse(partes[1].Split(':')[1].Replace("m", "").Trim());
                        int pressao = int.Parse(partes[2].Split(':')[1].Replace("Bar", "").Trim());

                        lock (lockObj)
                        {
                            var m = estadoMergulhadores.Find(x => x.Id == id);
                            if (m == null)
                            {
                                estadoMergulhadores.Add(new Mergulhador(id, profundidade, pressao));
                            }
                            else
                            {
                                m.Profundidade = profundidade;
                                m.PressaoCilindro = pressao;
                            }
                        }
                    }
                    catch { /* Ignora erros de parse corrompido na rede */ }
                }
            }
            finally { cliente.Close(); }
        }

        static void ProcessarDadosPesados()
        {
            while (true)
            {
                Thread.Sleep(3000);

                lock (lockObj)
                {
                    if (estadoMergulhadores.Count > 0)
                    {
                        Console.WriteLine("\n--- INICIANDO PROCESSAMENTO PESADO NO SERVIDOR ---");
                        
                        // 1. Bubble Sort O(N^2)
                        Console.WriteLine("A ordenar mergulhadores (Bubble Sort)...");
                        Sorters.BubbleSortByPressao(estadoMergulhadores);
                        
                        // 2. Análise Cruzada O(N^2)
                        Console.WriteLine("A calcular matriz de resgate cruzado...");
                        Analysis.AnalisarAutonomiaCruzada(estadoMergulhadores);

                        Console.WriteLine("--- FIM DO PROCESSAMENTO ---\n");
                    }
                }
            }
        }
    }
}