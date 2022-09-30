using LibraryManagementWeb.Data;
using LibraryManagementWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementWeb.Controllers
{
    public class BookController : Controller
    {
        private readonly Services _services;
        public readonly ApplicationDbContext _db;

        public BookController(Services services, ApplicationDbContext db)
        {
            _db = db;
            _services = services;
            services._db = _db;
        }

        public IActionResult Index(int page = 1)
        {
            var model = _services.Paging(page);
            ViewData["Pages"] = model.pages;
            ViewData["page"] = model.page;
            return View(model.books);
        }

        public IActionResult Detail(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            var book = _services.Get(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        public IActionResult Edit(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            var book = _services.Get(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BookModel book, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                _services.Upload(book, file);
                _db.Books.Update(book);
                _db.SaveChanges();
                TempData["success"] = "Edited successfully !";
                return RedirectToAction("Index");
            }
            return View(book);

        }

        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            var book = (_services.Get(id));
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int id)
        {
            if (id <= 0) { return NotFound(); }
            var book = _services.Get(id);
            if (book == null)
            {
                return NotFound();
            }
            string fileName = book.DataFile;
            string filePath = _services.GetDataPath(fileName);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            _db.Books.Remove(book);
            _db.SaveChanges();



            TempData["success"] = "Delete successfully !";
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BookModel book, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                _services.Upload(book, file);
                _db.Books.Add(book);
                _db.SaveChanges();
                TempData["success"] = "Created successfully !";
                return RedirectToAction("Index");
            }
            return View(book);
        }


        public IActionResult Search(string term)
        {
            return View("Index", _services.Get(term));
        }

        public IActionResult SaveIntoFile()
        {
            _services.SaveIntoFile();
            TempData["success"] = "Save successfully !";
            return RedirectToAction("Index");
        }

        public IActionResult Read(int id)
        {
            var b = _services.Get(id);
            if (b == null) return NotFound();
            if (!System.IO.File.Exists(_services.GetDataPath(b.DataFile))) return NotFound();
            var (stream, type) = _services.Download(b);
            return File(stream, type, b.DataFile);
        }

    }
}
