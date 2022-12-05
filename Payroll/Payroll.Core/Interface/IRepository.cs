using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Payroll.Core.Interface
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predecate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove();
        void RemoveRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);
    }
}
