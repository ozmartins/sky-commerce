using FluentAssertions;
using SkyCommerce.Loja.Domain.Produtos;
namespace SkyCommerce.Loja.Domain.Test.Produtos
{
    public class ProdutoTest
    {
        [Theory]
        [InlineData(100.01, 100.00, "")]
        [InlineData(100.00, 100.00, "")]
        [InlineData(095.00, 100.00, "")]
        [InlineData(090.00, 100.00, "10% OFF")]
        [InlineData(085.00, 100.00, "10% OFF")]
        [InlineData(080.00, 100.00, "20% OFF")]
        [InlineData(075.00, 100.00, "20% OFF")]
        [InlineData(070.00, 100.00, "30% OFF")]
        [InlineData(065.00, 100.00, "30% OFF")]
        [InlineData(060.00, 100.00, "40% OFF")]
        [InlineData(055.00, 100.00, "40% OFF")]
        [InlineData(050.00, 100.00, "50% OFF")]
        [InlineData(045.00, 100.00, "50% OFF")]
        [InlineData(040.00, 100.00, "60% OFF")]
        [InlineData(035.00, 100.00, "60% OFF")]
        [InlineData(030.00, 100.00, "70% OFF")]
        [InlineData(025.00, 100.00, "70% OFF")]
        [InlineData(020.00, 100.00, "80% OFF")]
        [InlineData(015.00, 100.00, "80% OFF")]
        [InlineData(010.00, 100.00, "90% OFF")]
        [InlineData(005.00, 100.00, "90% OFF")]
        public void DeveRetornarTextoPercentualDesconto(decimal preco, decimal precoAntigo, string textoEsperado)
        {
            //arrange
            var produto = new Produto
            {
                Valor = preco,
                ValorAntigo = precoAntigo
            };

            //act
            var texto = produto.TextoPercentualDesconto();

            //assert
            texto.Should().Be(textoEsperado);
        }

        [Theory]
        [InlineData(100.01, 100.00, 000)]
        [InlineData(100.00, 100.00, 000)]
        [InlineData(099.00, 100.00, 001)]
        [InlineData(095.00, 100.00, 005)]
        [InlineData(091.00, 100.00, 009)]
        [InlineData(090.00, 100.00, 010)]
        [InlineData(000.00, 100.00, 100)]
        public void DeveCalcularPercentualDesconto(decimal preco, decimal precoAntigo, decimal descontoEsperado)
        {
            //arrange
            var produto = new Produto
            {
                Valor = preco,
                ValorAntigo = precoAntigo
            };

            //act
            var desconto = produto.PercentualDesconto();

            //assert
            desconto.Should().Be(descontoEsperado);
        }

        [Theory]
        [InlineData(100.01, 100.00, false)]
        [InlineData(100.00, 100.00, false)]
        [InlineData(090.01, 100.00, false)]
        [InlineData(090.00, 100.00, true)]
        [InlineData(089.99, 100.00, true)]
        [InlineData(000.01, 100.00, true)]
        [InlineData(000.00, 100.00, true)]
        public void DeveIdentificarSeProdutoEstaEmPromocao(decimal preco, decimal precoAntigo, bool resultadoEsperado)
        {
            //arrange
            var produto = new Produto
            {
                Valor = preco,
                ValorAntigo = precoAntigo
            };

            //act
            var promocao = produto.Promocao();

            //assert
            promocao.Should().Be(resultadoEsperado);
        }

        [Theory]
        [InlineData(-1, false)]
        [InlineData(0, false)]
        [InlineData(1, true)]
        public void DeveIdentificarSeTemEstoque(int estoque, bool resultadoEsperado)
        {
            //arrange
            var produto = new Produto
            {
                Estoque = estoque
            };

            //act
            var temEstoque = produto.TemEstoque();

            //assert
            temEstoque.Should().Be(resultadoEsperado);
        }
    }
}