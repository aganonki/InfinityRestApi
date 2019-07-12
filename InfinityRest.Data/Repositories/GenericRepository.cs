using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using InfinityRest.Data.Data;
using InfinityRest.Data.Helpers;
using Microsoft.EntityFrameworkCore;

namespace InfinityRest.Data.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal InfinityDB Context;
         internal DbSet<TEntity> DbSet;
        public GenericRepository(InfinityDB context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get()
        {
            return DbSet.AsNoTracking().Take(100);
        }

        public virtual TEntity Get(object id)
        {
            return GetByEntity(id);
        }
        /// <summary>
        /// Generic get method on the basis of id for Entities.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity GetByID(object id)
        {
            return DbSet.Find(id);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = DbSet.Find(id);
            Delete(entityToDelete);
        }
        /// <summary>
        /// Generic Delete method for the entities
        /// </summary>
        /// <param name="entityToDelete"></param>
        public virtual void Delete(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }
            DbSet.Remove(entityToDelete);
        }

        /// <summary>
        /// generic delete method , deletes data for the entities on the basis of condition.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual void Delete(Func<TEntity, bool> where)
        {
            IQueryable<TEntity> objects = DbSet.Where<TEntity>(where).AsQueryable();
            foreach (TEntity obj in objects)
                DbSet.Remove(obj);
        }

        /// <summary>
        /// Generic update method for the entities
        /// </summary>
        /// <param name="entityToUpdate"></param>
        public virtual void Update(TEntity entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }
        /// <summary>
        /// update list of entities
        /// </summary>
        /// <param name="entities"></param>
        public virtual void Update(ICollection<TEntity> entities)
        {
            foreach (var item in entities)
            {
                Update(item);
            }
        }
        /// <summary>
        /// generic method to get many record on the basis of a condition.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where)
        {
            return DbSet.AsNoTracking().Where(where);
        }

        /// <summary>
        /// generic method to get many record on the basis of a condition but query able.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> GetManyQueryable(Expression<Func<TEntity, bool>> where)
        {
            return DbSet.AsNoTracking().Where(where);
        }

        /// <summary>
        /// generic get method , fetches data for the entities on the basis of condition.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual TEntity Get(Func<TEntity, Boolean> where)
        {
            return DbSet.AsNoTracking().SingleOrDefault<TEntity>(where);
        }

        /// <summary>
        /// generic method to fetch all the records from db
        /// </summary>
        public virtual IEnumerable<TEntity> GetAll()
        {
            return DbSet.AsNoTracking();
        }

        /// <summary>
        /// Include multiple
        /// </summary>
        public IQueryable<TEntity> GetWithInclude(
            System.Linq.Expressions.Expression<Func<TEntity,
            bool>> predicate, params string[] include)
        {
            IQueryable<TEntity> query = this.DbSet;
            query = include.Aggregate(query, (current, inc) => current.Include(inc));
            return query.Where(predicate);
        }

        /// <summary>
        /// Select only required columns from entities
        /// </summary>
        /// <typeparam name="TType">Type of output</typeparam>
        /// <param name="where">Search parameters</param>
        /// <param name="select">select statement</param>
        /// <returns>TType projection</returns>
        public virtual ICollection<TType> Get<TType>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TType>> select) where TType : class
        {
            return DbSet.AsNoTracking().Where(where).Select(select).ToList();
        }
        /// <summary>
        /// Select only required columns from entity
        /// </summary>
        public virtual TType Get<TType>(int where, Expression<Func<TEntity, TType>> select) where TType : class
        {
            var element =GetByID(where);
            return element.SelectSingle(select);
        }

        /// <summary>
        /// Generic method to check if entity exists
        /// </summary>
        public virtual bool Exists(object id)
        {
            return DbSet.Find(id) != null;
        }

        /// <summary>
        /// Gets a single record by the specified criteria (usually the unique identifier)
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        /// <returns>A single record that matches the specified criteria</returns>
        public virtual TEntity GetSingle(Func<TEntity, bool> predicate)
        {
            return DbSet.AsNoTracking().Single<TEntity>(predicate);
        }

        public virtual void Create(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual int Count()
        {
            return DbSet.Count();
        }
        /// <summary>
        /// create list of entities
        /// </summary>
        /// <param name="entities"></param>
        public virtual void Create(ICollection<TEntity> entities)
        {
            DbSet.AddRange(entities);
        }

        public virtual TEntity GetByEntity(object entity)
        {
            return DbSet.AsNoTracking().FirstOrDefault(x => x.Equals((TEntity)entity));
        }

        public virtual void SaveChanges()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (Exception e)
            {

                var outputLines = new List<string>();
                outputLines.Add(e.Message);
                //System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

                throw e;
            }
        }

        private bool disposed = false;

        /// <summary>
        /// Protected Virtual Dispose method
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Debug.WriteLine("UnitOfWork is being disposed");
                    Context.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
