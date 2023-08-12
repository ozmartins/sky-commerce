using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyCommerce.Loja.Domain
{
    public interface IStore<TEntity> : IDisposable
    {
        Task Adicionar(TEntity entity);
        Task Atualizar(TEntity entity);
        Task Remover(TEntity entity);
    }
}
