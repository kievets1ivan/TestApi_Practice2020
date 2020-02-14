using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestApi.DAL.Entities;

namespace TestApi.DAL
{
    public class AppDbContext : IdentityDbContext<UserEntity>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<ProductEntity> ProductSet { get; set; }
        public DbSet<TransactionEntity> TransactionSet { get; set; }
    }
}
