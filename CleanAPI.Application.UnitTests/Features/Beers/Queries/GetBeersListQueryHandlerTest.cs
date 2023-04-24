using AutoMapper;
using CleanAPI.Application.Features.Beers.Queries;
using CleanAPI.Application.Interfaces.Persistence;
using CleanAPI.Domain.Entities;
using NUnit.Framework;

namespace CleanAPI.Application.UnitTests.Features.Beers.Queries
{
    [TestFixture]
    public class GetBeersListQueryHandlerTest : TestBaseClass
    {
        public IBeersDbContext _context;
        public IMapper _mapper;

        [SetUp]
        public async Task SetUp() 
        {
            _mapper = GetMapper();
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
        public async Task Beers_RetrieveVmsList_WhenExistsDataInTheDb()
        {
            // Arrange
            var query = new GetBeersListQuery();
            var handler = new GetBeersListQueryHandler(_context, _mapper);

            // Act
            var res = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsTrue(res.Beers.Count > 0);
        }

        [Test]
        public async Task Beers_RetrieveEmptyList_WhenDoesntExistsData()
        {
            // Arrange
            _context = GetContext();
            var query = new GetBeersListQuery();
            var handler = new GetBeersListQueryHandler(_context, _mapper);

            // Act
            var res = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsTrue(res.Beers.Count == 0);
        }
    }
}

