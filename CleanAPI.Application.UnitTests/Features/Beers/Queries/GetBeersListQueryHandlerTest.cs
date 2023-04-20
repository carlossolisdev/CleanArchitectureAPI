using CleanAPI.Application.Interfaces.Persistence;
using CleanAPI.Domain.Entities;
using CleanAPI.Infraestructure.Persistence.Beers;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace CleanAPI.Application.UnitTests.Features.Beers.Queries
{
    [TestFixture]
    public class GetBeersListQueryHandlerTest : TestBaseClass
    {
        public IBeersDbContext _context;

        [SetUp]
        public async Task SetUp() 
        {
            // Create clean db
            _context = GetContext();

            // Seed data for tests
            var beersList = new List<Beer> {
                new Beer {
                    BeerId = 1,
                    Brand = "Brand",
                    Name = "Name",
                    Price = 100
                },
                new Beer {
                    BeerId = 2,
                    Brand = "Brand2",
                    Name = "Name2",
                    Price = 100
                },
            };

            _context.Beers.AddRange(beersList);
            await _context.SaveChangesAsync(CancellationToken.None);
        }

        [Test]
        public async Task GetBeersList()
        {
            var res = await _context.Beers.ToListAsync();
            Assert.IsTrue(res.Count == 1);
        }
    }
}
