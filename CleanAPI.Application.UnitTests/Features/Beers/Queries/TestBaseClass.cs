using CleanAPI.Application.Interfaces.Persistence;
using CleanAPI.Infraestructure.Persistence.Beers;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace CleanAPI.Application.UnitTests.Features.Beers.Queries
{
    public class TestBaseClass
    {
        protected IBeersDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<BeersDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDB")
                .Options;
            var context = new BeersDbContext(options);
            context.Database.EnsureDeleted();
            return context;
        }
    }
}