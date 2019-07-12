using System;
using System.Diagnostics;
using InfinityRest.Data.Data;
using InfinityRest.Data.Repositories;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace InfinityRest.BLManager
{
    public class UnitOfWork : IDisposable
    {
        #region Private member variables...

        public DatabaseFacade ContextTransaction => _context?.Database;
        private InfinityDB _context = null;
        private bool _disposed = false;
        private IRepository<Task> _taskRepository;
        private IRepository<Run> _runRepository;


        #endregion

        public UnitOfWork(InfinityDB context)
        {
            _context = context;
        }
        
        public IRepository<Task> TaskRepository
        {
            get
            {
                if (this._taskRepository == null)
                    this._taskRepository = new TaskRepository(_context);
                return _taskRepository;
            }
        }
        public IRepository<Run> RunRepository
        {
            get
            {
                if (this._runRepository == null)
                    this._runRepository = new RunRepository(_context);
                return _runRepository;
            }
        }

        /// <summary>
        /// Protected Virtual Dispose method
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    Debug.WriteLine("UnitOfWork is being disposed");
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {

                throw e;
            }

        }


    }
}
