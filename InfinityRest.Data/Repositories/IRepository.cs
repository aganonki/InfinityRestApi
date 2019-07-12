using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace InfinityRest.Data.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        IEnumerable<TEntity> Get();
        TEntity Get(object id);
        TEntity GetByID(object id);
        IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where);
        IQueryable<TEntity> GetManyQueryable(Expression<Func<TEntity, bool>> where);
        TEntity Get(Func<TEntity, bool> where);
        TEntity GetSingle(Func<TEntity, bool> predicate);
        TEntity GetByEntity(object entity);
        IEnumerable<TEntity> GetAll();

        IQueryable<TEntity> GetWithInclude(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate,
                params string[] include);

        ICollection<TType> Get<TType>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TType>> select)
                where TType : class;
        TType Get<TType>(int id, Expression<Func<TEntity, TType>> select)
                where TType : class;
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        void Delete(Func<TEntity, bool> where);
        void Update(TEntity entity);
        void Update(ICollection<TEntity> entities);
        void Create(TEntity entity);
        void Create(ICollection<TEntity> entities);
        int Count();
        void SaveChanges();
        bool Exists(object item);
    }
}
