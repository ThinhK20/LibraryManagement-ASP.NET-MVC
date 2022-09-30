using LibraryManagementWeb.Data;
using System.Xml.Serialization;

namespace LibraryManagementWeb.Models
{
    public class Services
    {
        public ApplicationDbContext _db { get; set; }

        public HashSet<BookModel> BooksInFile { get; set; }

        private readonly string _dataFile = @"Data\data.xml";
        private readonly XmlSerializer _serializer = new XmlSerializer(typeof(HashSet<BookModel>));

        public Services()
        {
            if (File.Exists(_dataFile))
            {
                using var stream = File.OpenRead(_dataFile);
                BooksInFile = _serializer.Deserialize(stream) as HashSet<BookModel>;
            }
            else
            {
                BooksInFile = new HashSet<BookModel>()
                {
                    new BookModel{Id=1, Title="ASP.NET Core for dummy", Authors = "Lewis", Publisher = "Washington", Year=2020},
                    new BookModel{Id=2, Title="Pro ASP.NET Core", Authors = "Manis", Publisher = "MVC Bioragi", Year=2016},
                    new BookModel{Id=3, Title="Fighting! Human", Authors = "Mena", Publisher="Ahn Desend", Year=2022},
                };

            }
        }

        public string GetDataPath(string file) => $"Data\\File\\{file}";

        public void Upload(BookModel book, IFormFile file)
        {
            if (file != null)
            {
                var path = GetDataPath(file.FileName);
                using var stream = new FileStream(path, FileMode.Create);
                file.CopyTo(stream);
                book.DataFile = file.FileName;
            }
        }

        public (Stream, string) Download(BookModel b)
        {
            var memory = new MemoryStream();
            using var stream = new FileStream(GetDataPath(b.DataFile), FileMode.Open);
            stream.CopyTo(memory);
            memory.Position = 0;
            var type = Path.GetExtension(b.DataFile) switch
            {
                "pdf" => "application/pdf",
                "docx" => "application/vnd.ms-word",
                "doc" => "application/vnd.ms-word",
                "txt" => "text/plain",
                _ => "application/pdf"
            };
            return (memory, type);
        }


        public void SaveIntoFile()
        {
            if (_db.Books != null)
            {
                BooksInFile = _db.Books.ToHashSet();
            }
            using var stream = File.Create(_dataFile);
            _serializer.Serialize(stream, BooksInFile);
        }

        public BookModel Get(int id)
        {
            return _db.Books.FirstOrDefault(b => b.Id == id);
        }

        public IEnumerable<BookModel> Get(string search)
        {
            if (search == "all" || search == null || search == "")
            {
                return _db.Books;
            }
            var s = search.ToLower();
            return _db.Books.Where(b =>
                b.Title.ToLower().Contains(s) ||
                b.Authors.ToLower().Contains(s) ||
                b.Publisher.ToLower().Contains(s) ||
                b.Description.Contains(s) ||
                b.Year.ToString() == s);
        }

        public (BookModel[] books, int pages, int page) Paging(int page)
        {
            int size = 5; // The maximum record in one page.
            int pages = (int)Math.Ceiling((double)_db.Books.Count() / size);
            var books = _db.Books.Skip((page - 1) * size).Take(size).ToArray();
            return (books, pages, page);
        }


    }
}
