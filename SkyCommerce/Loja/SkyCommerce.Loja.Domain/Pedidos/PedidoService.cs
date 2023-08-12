using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyCommerce.Loja.Domain.Pedidos
{
    internal class PedidoService
    {
        private readonly IPedidoStore _pedidoStore;
        public PedidoService(IPedidoStore pedidoStore)
        {
            _pedidoStore = pedidoStore;
        }
        public Task SalvarPedido(Pedido pedido, string usuario)
        { 
            return _pedidoStore.SalvarPedido(pedido, usuario);
        }
    }
}
