using Microsoft.EntityFrameworkCore;
using PMS.API2.Models;

namespace PMS.API2.Data
{
    public class PMSDbContext :DbContext
    {
        public PMSDbContext (DbContextOptions options):base(options)
        {
           
        }
        public DbSet<Product> Products { get; set; }
    }
}
