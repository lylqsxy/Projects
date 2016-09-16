using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Cobra.Infrastructure.Data;

namespace Cobra.Infrastructure
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class 
    {
        private IUnitOfWork _unitOfWork;
        internal DbContext db;
        internal IDbSet<TEntity> dbSet;
        internal DbEntityEntry<TEntity> dbEntry; 


        public Repository(IUnitOfWork uow)
        {
            _unitOfWork = uow;
            dbSet = uow.Set<TEntity>();
            db = uow.GetDbContext();
        }


        public TEntity GetById(int id)
        {
            TEntity model = dbSet.Find(id);
            return model;
        }

        public TEntity GetByName(string name)
        {
            TEntity model = dbSet.Find(name);
            return model;
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet.Where(predicate).AsEnumerable();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbSet.AsEnumerable();
        }

        public TEntity Add(TEntity model, bool persist = false)
        {
            dbSet.Add(model);
            Save(persist);

            return model;
        }

        public virtual void Update(TEntity entityToUpdate, bool persist = false)
        {
            var entry = db.Entry(entityToUpdate);

            if (entry.State == EntityState.Detached)
            {
                db.Set<TEntity>().Attach(entityToUpdate);
                entry.State = EntityState.Modified;
            }
            //Author: Aaron Bhardwaj
            //Set state of entity to modified, if not detached.
            //entry.State = EntityState.Modified;
            //Use DbWorkManager.Commit() to persist changes - Provides better control
            //Save(persist);
        }

        public void Remove(int id, bool persist = false)
        {
            TEntity model = dbSet.Find(id);
            Remove(model, persist);
        }

        public void Remove(TEntity model, bool persist = false)
        {
            if (model != null)
            {
                if (db.Entry(model).State == EntityState.Detached)
                {
                    dbSet.Attach(model);
                }
                dbSet.Remove(model);
            }
        }

        public void Save()
        {
            Save(true);
        }

        private void Save(bool persist)
        {
            if (persist)
            {
                db.SaveChanges();
            }
        }
    }
}
