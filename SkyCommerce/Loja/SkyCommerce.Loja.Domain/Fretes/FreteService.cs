using Microsoft.Extensions.Logging;
using Refit;
using SkyCommerce.Loja.Domain.Carrinhos;
using SkyCommerce.Loja.Domain.Extensions;
using SkyCommerce.Loja.Domain.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyCommerce.Loja.Domain.Fretes
{
    internal class FreteService
    {
        private readonly IProdutoStore _produtoStore;
        private readonly ILogger<FreteService> _logger;

        public FreteService(IProdutoStore produtoStore, ILogger<FreteService> logger)
        {
            _produtoStore = produtoStore;
            _logger = logger;
        }

        public Task<IEnumerable<DetalhesFrete>> ObterModalidades(string token)
        {
            var freteApi = RestService.For<IFreteApi>("https://localhost:5007");
            return freteApi.Modalidades($"Bearer {token}");
        }

        public Task<IEnumerable<Frete>> CalcularFrete(Embalagem embalagem, GeoCoordinate posicao, string token)
        {
            var httpClient = new HttpClient(new HttpLoggingHandler(_logger, new HttpClientHandler())) { BaseAddress = new Uri("https://localhost:5007") };
            var freteApi = RestService.For<IFreteApi>(httpClient);
            return freteApi.Calcular(posicao.Latitude, posicao.Longitude, embalagem, $"Bearer {token}");
        }

        public async Task<IEnumerable<Frete>> CalcularCarrinho(Carrinho carrinho, GeoCoordinate posicao, string token)
        {
            var httpClient = new HttpClient(new HttpLoggingHandler(_logger, new HttpClientHandler())) { BaseAddress = new Uri("https://localhost:5007") };
            var freteApi = RestService.For<IFreteApi>(httpClient);
            var fretes = (await freteApi.Modalidades($"Bearer {token}")).Select(m => new Frete(m.Modalidade, m.Descricao, 0)).ToList();
            if (carrinho is not null && posicao is not null)
            {
                foreach (var carrinhoItem in carrinho.Items)
                {
                    var produto = await _produtoStore.ObterPeloNome(carrinhoItem.NomeUnico);
                    var opcoesDeFrete = await freteApi.Calcular(posicao.Latitude, posicao.Longitude, produto.Embalagem, $"Bearer {token}");
                    foreach (var frete in fretes)
                    {
                        var modalidadeFreteEscolhida = opcoesDeFrete.FirstOrDefault(x => x.Modalidade.Equals(frete.Modalidade, StringComparison.InvariantCultureIgnoreCase));
                        frete.AtualizarValor(modalidadeFreteEscolhida);
                    }
                }
            }
            return fretes ?? new List<Frete>();
        }
    }
}
