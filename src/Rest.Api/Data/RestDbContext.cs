using Rest.Api.Models;
using Rest.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Rest.Api.Data
{
    public class RestDbContext: DbContext, IRestDbContext
    {
        public DbSet<User> Users { get; private set; }
        public DbSet<Transaction> Transactions { get; private set; }
        public RestDbContext(DbContextOptions options)
            :base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RestDbContext).Assembly);
        }
        
    }
}
