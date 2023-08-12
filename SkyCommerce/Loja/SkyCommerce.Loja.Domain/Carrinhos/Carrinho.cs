using SkyCommerce.Loja.Domain.Fretes;
using SkyCommerce.Loja.Domain.Produtos;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace SkyCommerce.Loja.Domain.Carrinhos
{
    public class Carrinho
    {
        public string Usuario { get; set; } = string.Empty;
        public List<ItemCarrinho> Items { get; set; } = new List<ItemCarrinho>();
        public Frete? Frete { get; set; }
        public string Cupom { get; set; } = string.Empty;
        public decimal PercentualDesconto { get; set; }
        public decimal Desconto { get; set; }

        public void AplicarCupom(string cupom)
        {
            Cupom = cupom;
            PercentualDesconto = 10;
            Desconto = Total() * PercentualDesconto / 100;
        }

        public void SelecionarFrete(Frete? frete)
        {
            Frete = frete;
        }

        public ItemCarrinho AtualizarQuantidade(string produto, int quantidade)
        {
            SelecionarFrete(null);
            var item = Items.FirstOrDefault(i => i.NomeUnico.Equals(produto, StringComparison.CurrentCultureIgnoreCase));
            if (item == null)
                throw new Exception($"Produto {produto} não encontrado.");
            item.Quantidade = quantidade;
            return item;
        }

        public ItemCarrinho AdicionarProduto(Produto produto, int quantidade)
        {
            SelecionarFrete(null);
            var item = new ItemCarrinho(produto, quantidade);
            Items.Add(item);
            return item;
        }

        public bool Possui(string nomeUnico) => Items.Any(x => x.NomeUnico.Equals(nomeUnico, StringComparison.CurrentCultureIgnoreCase));
        public bool FreteSelecionado() => TotalProdutos() >= 200 || Frete == null;
        public decimal TotalProdutos() => Items.Sum(i => i.Total());
        public decimal Impostos() => TotalProdutos() * 0.3m;
        public decimal TotalSemImpostos() => TotalProdutos() - Impostos();
        public decimal Total() => TotalProdutos() + ValorFrete() - Desconto;
        public decimal ValorFrete() => Frete?.Valor ?? 0;
        public IEnumerable<SnapshotProduto> Snapshot() => Items.Select(i => new SnapshotProduto(i));
    }
}
