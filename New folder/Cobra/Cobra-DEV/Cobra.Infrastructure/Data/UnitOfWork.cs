using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cobra.Model;

namespace Cobra.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private CobraEntities _dbContext;
        private bool _disposed = false;

        #region Lifetime

        public UnitOfWork(CobraEntities dbContext)
        {
            _dbContext = dbContext;
        }

        public DbContext GetDbContext()
        {
            return _dbContext;
        }
        #endregion

        #region Utilities

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public IDbSet<T> Set<T>() where T : class
        {
            return _dbContext.Set<T>();
        }

        public DbEntityEntry<T> Entry<T>(T entity) where T : class
        {
            return _dbContext.Entry<T>(entity);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
