using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyCommerce.Loja.Domain.Fretes
{
    public class Frete
    {
        public Frete(string modalidade, string descricao, decimal valor)
        {
            Modalidade = modalidade;
            Descricao = descricao;
            Valor = valor;
        }
        public Frete(DetalhesFrete detalhesFrete) : this(detalhesFrete.Modalidade, detalhesFrete.Descricao, 0)
        {            
        }

        public string Modalidade { get; set; }
        public string Descricao{ get; set; }
        public decimal Valor { get; set; }
        public void AtualizarValor(Frete? frete) 
        {
            Valor += frete?.Valor ?? 0;
        }
    }
}
