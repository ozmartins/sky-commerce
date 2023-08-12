using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SkyCommerce.Loja.Domain.Enderecos
{
    internal class Telefone
    {
        private static Regex _ddd = new Regex("\\([0-9]{2}\\)");
        private static Regex _numero = new Regex("(?<=\\([0-9]{2}\\))(.|[0-9]|-){0,11}$");
        public Telefone(string numero)
        {
            if (numero.IndexOfAny(new[] { '(', ')' }) < 0)
                throw new ArgumentException(nameof(numero));

            Numero = _numero.Match(numero).Value?.Trim() ?? string.Empty;
            DDD = _ddd.Match(numero).Value?.Replace("(", string.Empty).Replace(")", string.Empty) ?? string.Empty;
        }

        public string DDD { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public override string ToString() => $"({DDD}) {Numero}";        
    }
}
