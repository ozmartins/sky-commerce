using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkyCommerce.Loja.Domain.Avaliacoes;
using SkyCommerce.Loja.Domain.Marcas;

namespace SkyCommerce.Loja.Domain.Produtos
{
    public class Produto
    {
        public string Codigo { get; set; }
        public string Nome{ get; set; }
        public string NomeUnico { get; set; }
        public string Descricao{ get; set; }
        public string Imagem { get; set; }
        public decimal Valor{ get; set; }
        public decimal ValorAntigo { get; set; }
        public bool Novo { get; set; }
        public string Detalhes{ get; set; }
        public int Estoque{ get; set; }
        public HashSet<string> Imagens { get; set; }
        public HashSet<string> Cores { get; set; }
        public HashSet<string> Categorias { get; set; }
        public IEnumerable<Avaliacao> Avaliacoes{ get; set; }
        public Embalagem Embalagem { get; set; }
        public Marca Marca { get; set; }

        public decimal PercentualDesconto() 
        {
            var percDesc = 100 - (Valor / ValorAntigo * 100);
            return percDesc < 0 ? 0 : percDesc;
        }
        public bool Promocao() => PercentualDesconto() >= 10;
        public bool TemEstoque() => Estoque > 0;
        public string TextoPercentualDesconto() 
        {                      
            var percentualDescontoAjustado = Math.Truncate(PercentualDesconto());
            percentualDescontoAjustado -= percentualDescontoAjustado % 10;
            return percentualDescontoAjustado <= 0 ? string.Empty : $"{percentualDescontoAjustado}% OFF";
        }
    }
}
