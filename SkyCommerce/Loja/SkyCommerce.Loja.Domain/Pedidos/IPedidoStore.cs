using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyCommerce.Loja.Domain.Pedidos
{
    public interface IPedidoStore : IStore<Pedido>
    {
        Task SalvarPedido(Pedido pedido, string usuario);
        Task<Pedido> ObterPorIdentificador(string identificador, string usuario);
        Task<IEnumerable<Pedido>> ListarPedidos(string usuario);
        Task AtualizarStatus(string identificador, string usuario, StatusPedido status);
    }
}
