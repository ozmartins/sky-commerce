using Refit;
using SkyCommerce.Loja.Domain.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyCommerce.Loja.Domain.Fretes
{
    internal interface IFreteApi
    {
        [Get("/fretes/para/{latidade},{longitude}/calcular")]
        Task<IEnumerable<Frete>> Calcular(double latitude, double longitude, Embalagem embalagem, [Header("Authorization")]string token);
        
        [Get("/fretes")]
        Task<IEnumerable<DetalhesFrete>> Modalidades([Header("Authorization")] string token);
    }
}
