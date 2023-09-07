using Assignment2_Durham_Ryan.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assignment2_Durham_Ryan.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
