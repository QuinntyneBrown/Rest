using Rest.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;

namespace Rest.Api.Interfaces
{
    public interface IRestDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Transaction> Transactions { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);        
    }
}
