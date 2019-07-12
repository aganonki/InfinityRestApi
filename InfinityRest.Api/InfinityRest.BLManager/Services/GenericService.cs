using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using InfinityRest.BLManager.Interfaces;
using InfinityRest.Data.Repositories;

namespace InfinityRest.BLManager.Services
{
    public class GenericService<TRepository,TBusinessEntity,TEntity> : IService<TBusinessEntity>
        where TRepository : IRepository<TEntity>
        where TBusinessEntity : class
        where TEntity : class
    {
        private readonly UnitOfWork _unitOfWork;
        protected TRepository Repository;

        public GenericService(UnitOfWork unit, TRepository repository)
        {
            _unitOfWork = unit;
            Repository = repository;
        }

        public virtual TBusinessEntity GetById(int id)
        {
            var product = Repository.GetByID(id);
            var productModel = Mapper.Map<TBusinessEntity>(product);
            return productModel;
        }

        public virtual TType GetById<TType>(int productId, Expression<Func<TBusinessEntity, TType>> @where) where TType : class
        {
            
            var selectMapped = Mapper.Instance.MapExpression<Expression<Func<TEntity, TType>>>(where);
            var product = Repository.Get(productId,selectMapped);
            return product;
        }

        public virtual ICollection<TType> Get<TType>(Expression<Func<TBusinessEntity, bool>> where, Expression<Func<TBusinessEntity, TType>> select) where TType : class
        {
            var whereMapped = Mapper.Instance.MapExpression<Expression<Func<TEntity, bool>>>(where);
            var selectMapped = Mapper.Instance.MapExpression<Expression<Func<TEntity, TType>>>(select);
            var product = Repository.Get(whereMapped,selectMapped);
            return product;
        }

        public virtual ICollection<TBusinessEntity> GetAll()
        {
            var entities = Repository.GetAll();
            var result = new List<TBusinessEntity>();

            if (entities.Any())
            {
                result = Mapper.Map<List<TEntity>, List<TBusinessEntity>>(entities.ToList());
            }
            return result;
        }
        public virtual ICollection<TBusinessEntity> GetAll(Expression<Func<TBusinessEntity, bool>> where)
        {
            var whereMapped = Mapper.Instance.MapExpression<Expression<Func<TEntity, bool>>>(where);
            var entities = Repository.GetMany(whereMapped);

            
            var result = new List<TBusinessEntity>();

            if (entities.Any())
            {
                result = Mapper.Map<List<TEntity>, List<TBusinessEntity>>(entities.ToList());
            }
            return result;
        }

        public virtual int Create(TBusinessEntity entity)
        {
            using (var scope = _unitOfWork.ContextTransaction.BeginTransaction())
            {
                try
                {

                    var item = Mapper.Map<TEntity>(entity);
                    Repository.Create(item);
                    _unitOfWork.Save();
                    scope.Commit();
                    return 1;
                }
                catch (Exception e)
                {
                    var a = e;
                    throw;
                }
            }

            throw new NotImplementedException("Do not touc dis!");
        }

        public virtual bool Update(int entityId, TBusinessEntity entity)
        {
            var success = false;
            if (entity != null)
            {
                using (var scope = _unitOfWork.ContextTransaction.BeginTransaction())
                {
                    var product = Repository.GetByID(entityId);
                    if (product != null)
                    {
                        var item = Mapper.Map(entity, product);
                        Repository.Update(item);
                        _unitOfWork.Save();
                        scope.Commit();
                        success = true;
                    }
                }
            }
            return success;
        }

        public virtual bool Delete(int entityId)
        {
            var success = false;
            if (entityId > 0)
            {
                using (var scope = _unitOfWork.ContextTransaction.BeginTransaction())
                {
                    var item = Repository.GetByID(entityId);
                    if (item != null)
                    {
                        Repository.Delete(item);
                        _unitOfWork.Save();
                        scope.Commit();
                        success = true;
                    }
                }
            }
            return success;
        }

        public virtual long Count()
        {
            return Repository.Count();
        }
    }
}
