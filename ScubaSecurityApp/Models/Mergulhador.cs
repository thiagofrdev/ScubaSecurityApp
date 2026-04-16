using System;
using System.Collections.Generic;
using System.Text;

namespace ScubaSecurityApp.Models
{
    public class Mergulhador
    {
        public int Id { get; set; }
        public double Profundidade { get; set; }
        public double PressaoCilindro { get; set; }

        public Mergulhador(int id, double profundidade, double pressaoCilindro)
        {
            Id = id;
            Profundidade = profundidade;
            PressaoCilindro = pressaoCilindro;
        }

        public override string ToString()
        {
            return $"Mergulhador ID: {Id.ToString().PadLeft(2, '0')} | Profundidade: {Profundidade,5:F1}m | Pressão: {PressaoCilindro,5:F1} Bar";
        }
    }
}
