using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HandyMan.Core.Infrastructure
{
    public interface IRepository<TEntity>
    {
        //Create
        TEntity Add(TEntity entity);

        //read
        TEntity GetById(object id);
        TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> whereExpression);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetWhere(Expression<Func<TEntity, bool>> whereExpression);
        int Count();
        int Count(Expression<Func<TEntity, bool>> whereExpression);
        bool Any(Expression<Func<TEntity, bool>> whereExpression);

        //update
        void Update(TEntity entity);

        //delete
        void Delete(TEntity entity);
    }
}
