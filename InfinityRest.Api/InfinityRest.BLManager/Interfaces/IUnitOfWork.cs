using System;
using System.Collections.Generic;
using System.Text;
using InfinityRest.Data.Repositories;

namespace InfinityRest.BLManager.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        void Save();
    }
}
