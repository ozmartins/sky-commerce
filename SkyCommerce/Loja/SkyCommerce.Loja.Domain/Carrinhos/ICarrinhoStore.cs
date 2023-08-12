using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SkyCommerce.Loja.Domain.Carrinhos
{
    public interface ICarrinhoStore
    {
        Task<Carrinho> ObterCarrinho(string usuario);
        Task<Carrinho> CriarCarrinho(string usuario);
        Task AdicionarItemAoCarrinho(Carrinho carrinho, ItemCarrinho item);
        Task AtualizarItemCarrinho(ItemCarrinho item, Carrinho carrinho);
        Task Remover(string produto, string user);
        Task AtualizarCarrinho(Carrinho carrinho);
        Task RemoverTodosItens(string usuario);
    }
}
