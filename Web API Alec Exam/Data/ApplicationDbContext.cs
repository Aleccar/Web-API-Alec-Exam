using Microsoft.EntityFrameworkCore;
using Web_API_Alec_Exam.Models;

namespace Web_API_Alec_Exam.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options) 
        {
        }

        public DbSet<Ad> Ads { get; set; }
    }
}
