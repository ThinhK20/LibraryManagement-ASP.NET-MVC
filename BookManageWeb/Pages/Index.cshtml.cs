using BookManageWeb.Interfaces;
using BookManageWeb.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookManageWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IRepository _repository;

        public HashSet<Book> Books => _repository.Books;
        public int Count => _repository.Books.Count;

        public IndexModel(ILogger<IndexModel> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public void OnGet()
        {

        }
    }
}