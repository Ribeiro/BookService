using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BookService.Models;
using BookService.Repositories;
using BookService.Persistence;
using BookService.Filters;
using System.Diagnostics;
using Hangfire;

namespace BookService.Controllers
{
    public class BooksController : ApiController
    {
        private BookServiceContext db = new BookServiceContext();
        private readonly IUnitOfWork _unitOfWork;

        public BooksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Books
        public IEnumerable<Book> GetBooks()
        {
            long lastId = db.Books.Max(b => b.Id) + 1;

            var jobId = BackgroundJob.Enqueue(() => _unitOfWork.BooksRepository.Insert(BuildBookFom(lastId, "Teste_" + DateTime.Now, 1991, 10.00, "Comedy", null, 4)));
            BackgroundJob.ContinueWith(jobId, () => _unitOfWork.BooksRepository.Insert(BuildBookFom(lastId + 1, "Teste_" + DateTime.Now, 1992, 11.00, "Comedy", null, 4)));

            return _unitOfWork.BooksRepository.GetAll().Include(b => b.Author).ToList();
        }

        // GET: api/Books/5
        [ResponseType(typeof(Book))]
        public async Task<IHttpActionResult> GetBook(long id)
        {
            return Ok(await _unitOfWork.BooksRepository.FindAsync(id));
        }

        [ValidateModelState]
        // PUT: api/Books/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBook(long id, Book book)
        {
            db.Entry(book).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Books
        [ValidateModelState]
        [ResponseType(typeof(Book))]
        public async Task<IHttpActionResult> PostBook(Book book)
        {
            db.Books.Add(book);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = book.Id }, book);
        }

        // DELETE: api/Books/5
        [ResponseType(typeof(Book))]
        public async Task<IHttpActionResult> DeleteBook(long id)
        {
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            db.Books.Remove(book);
            await db.SaveChangesAsync();

            return Ok(new Book());
        }

        private bool BookExists(long id)
        {
            return db.Books.Count(e => e.Id == id) > 0;
            
        }

        private Book BuildBookFom(long id, string title, int year, double price, string genre, string note, long authorId)
        {
            Book book = new Book();
            book.Id = id;
            book.Title = title;
            book.Year = year;
            book.Price = price;
            book.Genre = genre;
            book.Note = note;
            book.AuthorId = authorId;
            return book;
        }
    }
}