using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SkyCommerce.Loja.Domain.Produtos
{
    public class Embalagem
    {
        public double Altura { get; set; }
        public double Largura{ get; set; }
        public double Comprimento{ get; set; }
        public double Peso { get; set; }
    }
}
