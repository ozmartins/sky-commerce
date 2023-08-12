using SkyCommerce.Loja.Domain.Avaliacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyCommerce.Loja.Domain.Produtos
{
    internal class ProdutoService
    {
        private readonly IAvaliacaoStore _avaliacaoStore;
        private readonly IProdutoStore _produtoStore;

        public ProdutoService(IAvaliacaoStore avaliacaoStore, IProdutoStore produtoStore)
        {
            _avaliacaoStore = avaliacaoStore;
            _produtoStore = produtoStore;
        }

        public async Task Comentar(Avaliacao avaliacao)
        {
            var produto = _produtoStore.ObterPeloNome(avaliacao.ProdutoUrl);
            if (produto != null)
                await _avaliacaoStore.Adicionar(avaliacao);
        }
    }
}
