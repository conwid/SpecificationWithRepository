using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecificationSample.Model
{
    public class BooksInitializer : CreateDatabaseIfNotExists<BookContext>
    {
        protected override void Seed(BookContext context)
        {
            base.Seed(context);
            context.Books.Add(new Book { Author = "Author1", Title = "Super book1" });
            context.Books.Add(new AudioBook { Author = "Author1", Title = "Super book2", Duration = 400 });
            context.Books.Add(new Book { Author = "Author2", Title = "Super book3" });
            context.Books.Add(new AudioBook { Author = "Author2", Title = "Super book4", Duration = 1000 });
        }
    }
    public class BookContext : DbContext
    {
        static BookContext()
        {
            Database.SetInitializer(new BooksInitializer());
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<AudioBook> AudioBooks { get; set; }
    }
}
