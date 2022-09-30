using BookManageWeb.Interfaces;
using BookManageWeb.Models;

namespace BookManageWeb.Repository
{
    public class BookRepository : IRepository
    {
        public HashSet<Book> Books { get; set; } = new HashSet<Book>
        {
            new Book {Id = 1, Title = "ASP.NET Core for dummy", Publisher = "Apress", Year = 2018, Authors = "Xiaomi"},
            new Book {Id = 2, Title = "Listening of the time", Publisher = "Sail", Year = 2016, Authors = "Mei"},
            new Book {Id = 3, Title = "Fighting with Myself", Publisher = "Person", Year = 2020, Authors = "Miami"},
            new Book {Id = 4, Title = "Wake up ! Human", Publisher = "August", Year = 2018, Authors = "Taiya"},
            new Book {Id = 5, Title = "Ashfall", Publisher = "Nami", Year = 2019, Authors = "Tiomi"},
        };

        public bool Add(Book book)
        {
            return Books.Add(book);
        }

        public Book Create()
        {
            var max = Books.Max(b => b.Id);
            var book = new Book() { Id = max + 1 };
            return book;
        }

        public bool Delete(int id)
        {
            var book = Get(id);
            return book != null ? Books.Remove(book) : false;
        }

        public Book Get(int id)
        {
            return Books.SingleOrDefault(book => book.Id == id);
        }

        public bool Update(Book book)
        {
            if (book != null)
            {
                Delete(book.Id);
                Add(book);
                return true;
            }
            return false;
        }
    }
}
