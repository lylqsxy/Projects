using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cobra.Model;

namespace Cobra.Infrastructure
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Add(TEntity model, bool persist = false);

        TEntity GetById(int id);
        TEntity GetByName(string name);

        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetAll();

        void Update(TEntity model, bool persist = false);

        void Remove(int id, bool persist = false);
        void Remove(TEntity model, bool persist = false);

        void Save();
        
    }
}
