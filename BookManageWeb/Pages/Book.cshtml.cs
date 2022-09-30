using BookManageWeb.Interfaces;
using BookManageWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookManageWeb.Pages
{
    public class BookModel : PageModel
    {
        public enum Action { Detail, Delete, Update, Create }
        private readonly IRepository _repository;
        public BookModel(IRepository repository)
        {
            _repository = repository;
        }

        public Action Job { get; private set; }
        public Book Book { get; private set; }
        public void OnGet(int id)
        {
            Job = Action.Detail;
            Book = _repository.Get(id);
            ViewData["Title"] = Book == null ? "Book not found!" : $"Detail - {Book.Title}";
        }

        public void OnGetDelete(int id)
        {
            Job = Action.Delete;
            Book = _repository.Get(id);
            ViewData["Title"] = Book == null ? "Book not found!" : $"Confirm deleting: {Book.Title}";
        }

        public IActionResult OnGetConfirm(int id)
        {
            _repository.Delete(id);
            return new RedirectToPageResult("index");
        }

        public void OnGetCreate()
        {
            Job = Action.Create;
            Book = _repository.Create();
            ViewData["Title"] = "Create a new book";
        }

        public IActionResult OnPostCreate(Book book)
        {
            _repository.Add(book);
            return new RedirectToPageResult("index");
        }

        public void OnGetUpdate(int id)
        {
            Job = Action.Update;
            Book = _repository.Get(id);
            ViewData["Title"] = "Update the book";
        }

        public IActionResult OnPostUpdate(Book book)
        {
            _repository.Update(book);
            return new RedirectToPageResult("index");
        }
    }
}
