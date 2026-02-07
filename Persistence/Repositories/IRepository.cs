using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public interface IRepository<TEntity, TId>
    {
        public Task AddAsync(TEntity entity);
        public Task<List<TEntity>> GetAll();
        public Task UpdateAsync(TEntity entity);
        public Task DeleteAsync(TId id);
    }
}
