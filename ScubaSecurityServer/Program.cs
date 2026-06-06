using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ScubaSecurityServer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("=============================================");
            Console.WriteLine(" SCUBA SECURITY - SERVIDOR ");
            Console.WriteLine("=============================================\n");

            int porta = 8080;
            TcpListener listener = new TcpListener(IPAddress.Any, porta);

            try
            {
                listener.Start();
                Console.WriteLine($"[SERVIDOR] Iniciado. Aguardando dados dos mergulhadores na porta {porta}...\n");

                while (true)
                {
                    TcpClient cliente = await listener.AcceptTcpClientAsync();
                    
                    // Dispara uma Task separada para ler os dados desse cliente específico. Permitindo que o servidor atenda os 10 clientes ao mesmo tempo.
                    _ = Task.Run(() => TratarCliente(cliente));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERRO SERVIDOR] {ex.Message}");
            }
            finally
            {
                listener.Stop();
            }
        }

        /// <summary>
        /// Lê os dados enviados por um mergulhador específico de forma contínua.
        /// 
        /// Complexidade: O(M) onde M é o número de mensagens enviadas por este cliente.
        /// Justificativa: O laço 'while' itera linearmente sobre o fluxo de dados recebido pela rede.
        /// </summary>
        static void TratarCliente(TcpClient cliente)
        {
            try
            {
                using NetworkStream stream = cliente.GetStream();
                using StreamReader reader = new StreamReader(stream);

                while (cliente.Connected)
                {
                    string? mensagem = reader.ReadLine();
                    
                    if (mensagem == null) 
                        break; 

                    Console.WriteLine($"[DADO INTERCEPTÁVEL] {mensagem}");
                }
            }
            catch (IOException)
            {
                Console.WriteLine("[AVISO] Conexão com um mergulhador perdida.");
            }
            finally
            {
                cliente.Close();
            }
        }
    }
}