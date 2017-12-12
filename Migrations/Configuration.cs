namespace BookService.Migrations
{
    using BookService.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BookService.Models.BookServiceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BookService.Models.BookServiceContext context)
        {
             context.Authors.AddOrUpdate(x => x.Id,
                 new Author() { Id = 4, Name = "Jane Austen" },
                 new Author() { Id = 5, Name = "Charles Dickens" },
                 new Author() { Id = 6, Name = "Miguel de Cervantes" }
             );

             context.Books.AddOrUpdate(x => x.Id,
                    new Book()
                    {
                        Id = 5,
                        Title = "Pride and Prejudice",
                        Year = 1813,
                        AuthorId = 4,
                        Price = 9.99,
                        Genre = "Comedy of manners"
                    },
                    new Book()
                    {
                        Id = 6,
                        Title = "Northanger Abbey",
                        Year = 1817,
                        AuthorId = 4,
                        Price = 12.95,
                        Genre = "Gothic parody"
                    },
                    new Book()
                    {
                        Id = 7,
                        Title = "David Copperfield",
                        Year = 1850,
                        AuthorId = 5,
                        Price = 15,
                        Genre = "Bildungsroman"
                    },
                    new Book()
                    {
                        Id = 8,
                        Title = "Don Quixote",
                        Year = 1617,
                        AuthorId = 6,
                        Price = 8.95,
                        Genre = "Picaresque"
                    }
             );

        }
    }
}
