using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobra.Infrastructure.Data
{
    public interface IUnitOfWork
    {
        DbContext GetDbContext();
        DbEntityEntry<T> Entry<T>(T entity) where T : class;
        IDbSet<T> Set<T>() where T : class;
        void Commit();

        void Dispose();
    }
}
