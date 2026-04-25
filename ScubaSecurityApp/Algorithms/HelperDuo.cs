using System;
using System.Collections.Generic;
using ScubaSecurityApp.Models;

namespace ScubaSecurityApp.Algorithms
{
    public static class Analysis
    {
        /// <summary>
        /// Realiza uma análise cruzada entre todos os mergulhadores para identificar parceiros de resgate viáveis.
        /// 
        /// Complexidade: O(N²)
        /// Razão: O algoritmo utiliza dois laços 'for' aninhados para comparar cada mergulhador
        /// com todos os outros mergulhadores da lista. Para N mergulhadores, realizamos N * (N-1) comparações.
        /// 
        /// Consequências: Caso o número de mergulhadores monitorados cresça para milhares, o tempo de 
        /// processamento aumentará quadraticamente, podendo causar travamentos no sistema de monitoramento em tempo real.
        /// </summary>
        public static void AnalisarAutonomiaCruzada(List<Mergulhador> mergulhadores)
        {
            for (int i = 0; i < mergulhadores.Count; i++)
            {
                Mergulhador m1 = mergulhadores[i];
                
                for (int j = 0; j < mergulhadores.Count; j++)
                {
                    // Não comparar o mergulhador com ele mesmo
                    if (i == j) continue;

                    Mergulhador m2 = mergulhadores[j];

                    // Lógica de Autonomia: Um mergulhador pode ajudar se tiver mais de 100 Bar 
                    // e estiver em uma profundidade similar (diferença menor que 5 metros)
                    double diferencaProfundidade = m1.Profundidade - m2.Profundidade;
                    if (diferencaProfundidade < 0) diferencaProfundidade *= -1;

                    if (m2.PressaoCilindro > 100 && diferencaProfundidade <= 5.0)
                    {
                        Console.WriteLine(FormatarConexaoResgate(m1, m2));
                        //Console.WriteLine($"Dupla Encontrada: {m1.Id:00} ({m1.PressaoCilindro,5:F1}Bar)|{m1.Profundidade,5:F1}Metros) ajuda {m2.Id:00} ({m2.PressaoCilindro,5:F1}Bar)|{m2.Profundidade,5:F1}Metros)");
                    }
                }
            }
        }
        
        /// <summary>
        /// Formata visualmente a conexão de resgate entre dois mergulhadores.
        /// </summary>
        /// <remarks>
        /// Complexidade: O(1) - Apenas formatação de string.
        /// </remarks>
        public static string FormatarConexaoResgate(Mergulhador m1, Mergulhador m2)
        {
            string ajudante = $"[M{m2.Id:00} | {m2.PressaoCilindro,5:F1} Bar | {m2.Profundidade,4:F1}m]";
            string ajudado = $"[M{m1.Id:00} | {m1.PressaoCilindro,5:F1} Bar | {m1.Profundidade,4:F1}m]";

            return $"{ajudante}  AUXILIA -->  {ajudado}";
        }
    }
}