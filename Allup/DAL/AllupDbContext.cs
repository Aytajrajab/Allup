using Allup.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace Allup.DAL
{
    public class AllupDbContext : DbContext
    {
        public AllupDbContext(DbContextOptions<AllupDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
    }
}
