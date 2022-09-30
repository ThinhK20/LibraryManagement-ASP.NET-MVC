using LibraryManagementWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<BookModel> Books { get; set; }
    }
}
