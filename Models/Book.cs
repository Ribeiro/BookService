using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BookService.Models
{
    public class Book : BaseModel   
    {
        public long Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int Year { get; set; }
        public double Price { get; set; }
        public string Genre { get; set; }
        public string Note { get; set; }

        // Foreign Key
        public long AuthorId { get; set; }
        // Navigation property
        public Author Author { get; set; }

        public static implicit operator Task<object>(Book v)
        {
            throw new NotImplementedException();
        }
    }
}