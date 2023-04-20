using CleanAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanAPI.Application.Interfaces.Persistence
{
    public interface IDbContext : IDisposable
    { 
        DbContext Instance { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }

    public  interface IBeersDbContext : IDbContext
    {
        public DbSet<Beer> Beers { get; set; }
    }
}
