using SkyCommerce.Loja.Domain.Marcas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyCommerce.Loja.Domain.Produtos
{
    public interface IProdutoStore : IStore<Produto>
    {
        Task<IEnumerable<Produto>> ObterTodos();
        Task<IEnumerable<Produto>> ObterPelaCategoria(string categoria);
        Task<Produto> ObterPeloNome(string nome);
    }
}
