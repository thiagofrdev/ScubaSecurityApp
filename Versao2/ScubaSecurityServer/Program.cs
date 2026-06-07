using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Threading;
using ScubaSecurityServer.Models;
using ScubaSecurityServer.Security;

namespace ScubaSecurityServer
{
    class Program
    {
        static List<Mergulhador> estadoMergulhadores = new List<Mergulhador>();
        static readonly object lockObj = new object();

        static async Task Main(string[] args)
        {
            Console.WriteLine("=============================================");
            Console.WriteLine(" SCUBA SECURITY - SERVIDOR (V2 - Seguro e Otimizado) ");
            Console.WriteLine("=============================================\n");

            try {
                CryptoHelper.InicializarComRuidoAcustico("mergulho.wav");
                Console.WriteLine("[SEGURANÇA] Criptografia baseada em entropia acústica ATIVADA.\n");
            } catch (Exception ex) {
                Console.WriteLine($"[ERRO DE ÁUDIO] {ex.Message}. Verifica se o ficheiro .wav está na pasta.");
                return;
            }

            int porta = 8080;
            TcpListener listener = new TcpListener(IPAddress.Any, porta);

            try
            {
                listener.Start();
                Console.WriteLine($"[SERVIDOR] A escutar na porta {porta}...\n");

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
                using StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };

                while (cliente.Connected)
                {
                    string? mensagemCifrada = reader.ReadLine();
                    if (mensagemCifrada == null) break; 

                    Console.WriteLine($"\n[REDE - Intercetado] {mensagemCifrada}");
                    
                    string mensagemPlana = CryptoHelper.Decriptar(mensagemCifrada);
                    Console.WriteLine($"[SISTEMA - Decriptado] {mensagemPlana}");

                    string[] partes = mensagemPlana.Split('|');
                    int id = int.Parse(partes[0].Split(':')[1].Trim());
                    double profundidade = double.Parse(partes[1].Split(':')[1].Replace("m", "").Trim());
                    double pressao = double.Parse(partes[2].Split(':')[1].Replace("Bar", "").Trim());

                    lock (lockObj)
                    {
                        var m = estadoMergulhadores.Find(x => x.Id == id);
                        if (m == null) estadoMergulhadores.Add(new Mergulhador(id, profundidade, pressao));
                        else { m.Profundidade = profundidade; m.PressaoCilindro = pressao; }
                        
                        List<string> dadosCompactos = new List<string>();
                        foreach (var merg in estadoMergulhadores)
                            dadosCompactos.Add($"{merg.Id},{merg.Profundidade},{merg.PressaoCilindro}");
                        
                        string respostaPlana = string.Join(";", dadosCompactos);
                        string respostaCifrada = CryptoHelper.Encriptar(respostaPlana);
                        
                        writer.WriteLine(respostaCifrada);
                    }
                }
            }
            catch { /* Ignora desconexões */ }
            finally { cliente.Close(); }
        }
    }
}