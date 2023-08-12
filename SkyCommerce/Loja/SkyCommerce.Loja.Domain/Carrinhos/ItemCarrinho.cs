using SkyCommerce.Loja.Domain.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyCommerce.Loja.Domain.Carrinhos
{
    public class ItemCarrinho
    {
        public ItemCarrinho(Produto produto, int quantidade)
        {
            NomeUnico = produto.NomeUnico;
            Produto = produto.Nome;
            Imagem = produto.Imagem;
            Quantidade = quantidade;
            Valor = produto.Valor;
        }
        public string NomeUnico { get; }
        public string Produto { get; }
        public string Imagem { get; }        
        public decimal Valor { get; }

        public int Quantidade
        {
            get
            {
                return Quantidade;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("value");
                Quantidade = value;
            }
        }

        public decimal Total() => Quantidade * Valor;
    }
}
