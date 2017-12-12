using BookService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Threading.Tasks;

namespace BookService.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(DbContext context) : base(context)
        {
        }

        public override async Task<Book> FindAsync(long Id)
        {
            Book bookFromDB = await context.Set<Book>().Include(b => b.Author).Where(b => b.Id == Id).FirstOrDefaultAsync();
            AssertNotMissingRecordOn(bookFromDB);
            return bookFromDB;
        }

        public override Book Get(long id)
        {
            return context.Set<Book>().Include(b => b.Author).Where(x => x.Id == id).FirstOrDefault();
        }

    }
}