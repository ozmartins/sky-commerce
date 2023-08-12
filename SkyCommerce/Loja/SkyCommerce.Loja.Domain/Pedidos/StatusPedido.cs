using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyCommerce.Loja.Domain.Pedidos
{
    public enum StatusPedido
    {
        Processando = 1,
        SeparandoParaEnvio = 2,
        Enviado = 4,
        Finalizado = 5,
        Cancelado = 6,
        PagamentoNegado = 7,
        AguardandoConfirmacao = 8
    }
}
