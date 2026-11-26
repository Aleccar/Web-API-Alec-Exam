using Microsoft.EntityFrameworkCore;
using Web_API_Alec_Exam.Models;

namespace Web_API_Alec_Exam.Data
{
    public class DataInitializer
    {
        private readonly ApplicationDbContext _dbContext;

        public DataInitializer(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void MigrateData()
        {
            _dbContext.Database.Migrate();
            SeedData();
            _dbContext.SaveChanges();
        }

        public void SeedData()
        {
            if(! _dbContext.Ads.Any())
            {
                _dbContext.Add(new Ad
                {
                    Title = "Big boat for sale",
                    Description = "I'm be sellin mah big boat, for cheaps!",
                    CreatedAt = DateTime.Now,
                    Price = 1500m
                });
                _dbContext.Add(new Ad
                {
                    Title = "Old book collection",
                    Description = "I need space for new books so I am selling some oldies but goodies.",
                    CreatedAt = DateTime.Now.AddDays(-1),
                    Price = 200m
                });
                _dbContext.Add(new Ad
                {
                    Title = "Selling Hannes' old socks",
                    Description = "They've seen some use, but are still functional?",
                    CreatedAt = DateTime.Now,
                    Price = 20m
                });
            }
        }
    }
}
