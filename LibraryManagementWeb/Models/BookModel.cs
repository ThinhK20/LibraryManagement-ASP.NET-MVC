using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementWeb.Models
{
    public class BookModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Authors { get; set; }
        [Required]
        public string Publisher { get; set; }
        [Required]
        [Range(1990, maximum: int.MaxValue, ErrorMessage = $"The year must in above 1990")]
        public int Year { get; set; } = DateTime.Now.Year;
        public string Description { get; set; } = "";
        [DisplayName("Data file")]
        public string DataFile { get; set; } = "";
    }
}
