using Microsoft.EntityFrameworkCore;
using MOM_PROJECT.Models;

namespace MOM_PROJECT.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }
    }
}