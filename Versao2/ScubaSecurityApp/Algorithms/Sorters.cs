using System;
using System.Collections.Generic;
using ScubaSecurityApp.Models;

namespace ScubaSecurityApp.Algorithms
{
    public static class Sorters
    {
        /// <summary>
        /// Implementação manual do algoritmo Bubble Sort para ordenação crescente por pressão.
        /// 
        /// Complexidade: O(N²)
        /// Razão: O algoritmo utiliza dois laços de repetição aninhados. 
        /// No pior caso, cada elemento da lista de tamanho N é comparado com todos os outros.
        /// 
        /// Consequências: Para entradas de dados muito grandes, o tempo de execução cresce 
        /// quadraticamente, tornando o programa lento e ineficiente comparado a algoritmos 
        /// de divisão e conquista como o QuickSort.
        /// </summary>
        /// <param name="lista">Lista de mergulhadores a ser ordenada.</param>
        public static void BubbleSortByPressao(List<Mergulhador> lista)
        {
            int n = lista.Count;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    // Comparação dos valores numéricos de pressão
                    if (lista[j].PressaoCilindro > lista[j + 1].PressaoCilindro)
                    {
                        // Troca dos objetos na lista
                        Mergulhador temp = lista[j];
                        lista[j] = lista[j + 1];
                        lista[j + 1] = temp;
                    }
                }
            }
        }
    }
}