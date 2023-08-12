using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SkyCommerce.Loja.Domain.Marcas
{
    public interface IMarcaStore : IStore<Marca>
    {
        Task<IEnumerable<Marca>> ObterTodas();
        Task<Marca> ObterPeloNome(string nome);
    }
}
