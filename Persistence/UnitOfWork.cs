using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookService.Repositories;
using System.Data.Entity;
using BookService.Models;

namespace BookService.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext context;

        public IBookRepository BooksRepository { get; private set; }

        public IAuthorRepository AuthorsRepository { get; private set; }

        public UnitOfWork(BookServiceContext context)
        {
            this.context = context;
            BooksRepository = new BookRepository(context);
            AuthorsRepository = new AuthorRepository(context);
            
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public int Save()
        {
            return context.SaveChanges();
        }
    }
}