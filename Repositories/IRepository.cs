using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookService.Repositories
{
    public interface IRepository<T>
    {
        T Get(long id);

        IQueryable<T> GetAll();

        IEnumerable<T> Fetch(Expression<Func<T, bool>> predicate);

        void Insert(T obj);

        void InsertCollection(IEnumerable<T> collection);

        T update(T obj);

        void Delete(T obj);

        void DeleteCollection(IEnumerable<T> collection);

        T SingleOrDefault(Expression<Func<T, bool>> predicate);

        T FirstOrDefault(Expression<Func<T, bool>> predicate);

        Task<T> FindAsync(long Id);
    }
}
