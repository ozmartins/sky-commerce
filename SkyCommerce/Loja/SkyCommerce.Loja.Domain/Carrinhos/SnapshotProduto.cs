using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyCommerce.Loja.Domain.Carrinhos
{
    public class SnapshotProduto
    {
        public SnapshotProduto(ItemCarrinho item)
        {
            Nome = item.Produto;
            NomeUnico = item.NomeUnico;
            Imagem = item.Imagem;
            Valor = item.Valor;
            Quantidade = item.Quantidade;
        }

        public string Nome { get; set; }
        public string NomeUnico { get; set; }
        public string Imagem{ get; set; }
        public decimal Valor{ get; set; }
        public int Quantidade{ get; set; }
        public decimal Total() => Quantidade * Valor;

    }
}
