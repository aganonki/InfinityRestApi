using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace InfinityRest.BLManager.Interfaces
{

    public interface IService<TEntity> where TEntity : class
    {
        TEntity GetById(int productId);
        TType GetById<TType>(int productId,Expression<Func<TEntity, TType>> where) where TType : class;
        ICollection<TType> Get<TType>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TType>> select) where TType : class;
        //PagedResult GetAll([Optional]PaginationArgs args);
        ICollection<TEntity> GetAll();
        ICollection<TEntity> GetAll(Expression<Func<TEntity, bool>> where);
        int Create(TEntity entity);
        bool Update(int entityId, TEntity entity);
        bool Delete(int entityId);
        long Count();
    }

    public interface IGenericListService<TEntity> : IService<TEntity>
            where TEntity : class
    {
        int[] Create(ICollection<TEntity> entities);
        bool Update(ICollection<TEntity> entities);
        bool Delete(List<Int64> entityids);
    }
}
