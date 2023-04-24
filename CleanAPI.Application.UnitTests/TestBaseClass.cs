using AutoMapper;
using CleanAPI.Application.Interfaces.Persistence;
using CleanAPI.Infraestructure.Persistence.Beers;
using Microsoft.EntityFrameworkCore;
using static CleanAPI.Application.Features.Beers.Queries.BeerDto;

namespace CleanAPI.Application.UnitTests
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

        protected IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MapperClass>());
            var mapper = config.CreateMapper();
            return mapper;
        }
    }
}