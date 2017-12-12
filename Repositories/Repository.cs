using BookService.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

namespace BookService.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbContext context;

        public Repository(DbContext context)
        {
            this.context = context;
        }

        public void Delete(T obj)
        {
            context.Set<T>().Remove(obj);
        }

        public void DeleteCollection(IEnumerable<T> collection)
        {
            context.Set<T>().RemoveRange(collection);
        }

        public IEnumerable<T> Fetch(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().Where(predicate);
        }

        public virtual Task<T> FindAsync(long Id)
        {
            Task<T> resultTask = context.Set<T>().FindAsync(Id);
            AssertNotMissingRecordOn(resultTask);
            return resultTask;
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().FirstOrDefault(predicate);
        }

        public virtual T Get(long id)
        {
            return context.Set<T>().Find(id);
        }

        public IQueryable<T> GetAll()
        {
            return context.Set<T>();
        }

        public void Insert(T obj)
        {
            context.Set<T>().Add(obj);
        }

        public void InsertCollection(IEnumerable<T> collection)
        {
            context.Set<T>().AddRange(collection);
        }

        public T SingleOrDefault(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().SingleOrDefault(predicate);
        }

        public T update(T obj)
        {
            context.Entry<T>(obj).State = EntityState.Modified;
            return obj;
        }

        protected void AssertNotMissingRecordOn(object result)
        {
            if (null == result)
            {
                throw new ResourceNotFoundException();
            }

        }
    }
}