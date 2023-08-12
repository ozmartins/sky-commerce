using SkyCommerce.Loja.Domain.Fretes;
using SkyCommerce.Loja.Domain.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Threading.Tasks;

namespace SkyCommerce.Loja.Domain.Carrinhos
{
    internal class CarrinhoService 
    {
        private readonly ICarrinhoStore _carrinhoStore;
        private readonly FreteService _freteService;
        public CarrinhoService(
            ICarrinhoStore carrinhoStore,
            FreteService freteService)
        {
            _carrinhoStore = carrinhoStore;
            _freteService = freteService;
        }

        public async Task AdicionarProduto(string usuario, Produto produto, int quantidade)
        {
            var carrinho = await _carrinhoStore.ObterCarrinho(usuario);

            if (carrinho == null)
                carrinho = await _carrinhoStore.CriarCarrinho(usuario);

            var freteEscolhido = carrinho.Frete?.Modalidade;

            if (carrinho.Possui(produto.NomeUnico))
            {
                var item = carrinho.AtualizarQuantidade(produto.NomeUnico, quantidade);

                if (!string.IsNullOrEmpty(freteEscolhido))
                    await _carrinhoStore.AtualizarItemCarrinho(item, carrinho);
            }
            else
            {
                var item = carrinho.AdicionarProduto(produto, quantidade);
                await _carrinhoStore.AdicionarItemAoCarrinho(carrinho, item);
            }

        }

        public async Task AtualizarQuantidadeProduto(string usuario, string produto, int quantidade)
        {
            var carrinho = await _carrinhoStore.ObterCarrinho(usuario);

            if (carrinho == null)
                carrinho = await _carrinhoStore.CriarCarrinho(usuario);

            if (carrinho.Possui(produto))
            {
                var item = carrinho.AtualizarQuantidade(produto, quantidade);

                if (quantidade <= 0)
                    await _carrinhoStore.Remover(produto, usuario);
                else
                    await _carrinhoStore.AtualizarItemCarrinho(item, carrinho);
            }
        }

        public async Task SelecionarFrete(string usuario, string modalidade, GeoCoordinate geolocalizarUsuario, string token)
        {
            var carrinho = await _carrinhoStore.ObterCarrinho(usuario);
            var fretes = await _freteService.CalcularCarrinho(carrinho, geolocalizarUsuario, token);
            carrinho.SelecionarFrete(fretes.FirstOrDefault(f => f.Modalidade.Equals(modalidade)));
            await _carrinhoStore.AtualizarCarrinho(carrinho);
        }

        public async Task LimparCarrinho(string usuario)
        {
            await _carrinhoStore.RemoverTodosItens(usuario);
        }

        public async Task Remover(string produto, string user)
        {
            var carrinho = await _carrinhoStore.ObterCarrinho(user);
            if (carrinho.Possui(produto))
            {
                await _carrinhoStore.Remover(produto, user);
            }
        }

        public async Task AplicarCupom(string cupom, string user)
        {
            var carrinho = await _carrinhoStore.ObterCarrinho(user);

            if (carrinho == null)
                return;

            carrinho.AplicarCupom(cupom);
            await _carrinhoStore.AtualizarCarrinho(carrinho);
        }
    }
}
