using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyCommerce.Loja.Domain.Fretes
{
    public class DetalhesFrete
    {
        public bool Ativo { get; set; }
        public string Modalidade { get; set; } = string.Empty;
        public string Descricao{ get; set; } = string.Empty;
        public decimal ValorMinimo { get; set; }
        public decimal Multilicador{ get; set; }
    }
}
