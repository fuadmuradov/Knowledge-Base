using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Core.IRepositories
{
    public interface IRepository<TEntity>
    {
        Task AddAsync(TEntity entity);
        void Delete(TEntity entity);
        Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> exp, params string[] includes);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> exp, params string[] includes);

        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> exp, params string[] includes);

        Task<int> SaveDbAsync();
        int SaveDb();
        
    }
}
