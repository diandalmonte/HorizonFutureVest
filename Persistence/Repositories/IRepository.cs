using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public interface IRepository<TEntity, TId>
    {
        public Task AddAsync(TEntity);
        public Task<List<TEntity>> GetAll();
        public Task<TEntity> GetById(TId id);
        public Task UpdateAsync(TId id);
        public Task DeleteAsync(TId id);
    }
}
