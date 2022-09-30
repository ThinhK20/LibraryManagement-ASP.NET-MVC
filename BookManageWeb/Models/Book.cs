namespace BookManageWeb.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = "A new book";
        public string Authors { get; set; } = "Authors";
        public string Publisher { get; set; } = "Publisher";
        public int Year { get; set; } = DateTime.Now.Year;
        public string Description { get; set; } = "";
    }
}
