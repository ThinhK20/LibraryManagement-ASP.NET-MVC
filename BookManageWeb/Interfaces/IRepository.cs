using BookManageWeb.Models;

namespace BookManageWeb.Interfaces
{
    public interface IRepository
    {
        public HashSet<Book> Books { get; set; }
        public Book Get(int id);

        public bool Delete(int id);
        public bool Add(Book book);
        public Book Create();
        public bool Update(Book book);
    }
}
