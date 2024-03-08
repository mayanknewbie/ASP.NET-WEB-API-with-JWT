using Microsoft.EntityFrameworkCore;

namespace JWT_Demo.Models
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> EmployeeData { get; set; }
    }
}
