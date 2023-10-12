using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MVC_CRUD.Models;

namespace MVC_CRUD.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
