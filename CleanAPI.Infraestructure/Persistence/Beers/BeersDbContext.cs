using CleanAPI.Application.Interfaces.Persistence;
using CleanAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CleanAPI.Infraestructure.Persistence.Beers
{
    public class BeersDbContext : DbContext, IBeersDbContext
    {
        public BeersDbContext(DbContextOptions<BeersDbContext> options)
            : base(options)
        {
            
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //}

        public DbContext Instance => this;

        public DbSet<Beer> Beers { get; set; }
    }
}