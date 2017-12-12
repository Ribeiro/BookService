using BookService.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookService.Persistence
{
    public interface IUnitOfWork : IDisposable
    {

        IBookRepository BooksRepository { get; }
        IAuthorRepository AuthorsRepository { get; }
        int Save();
    }
}
