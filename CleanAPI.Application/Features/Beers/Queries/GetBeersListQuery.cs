using AutoMapper;
using CleanAPI.Application.Interfaces.Persistence;
using CleanAPI.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanAPI.Application.Features.Beers.Queries
{
    public class GetBeersListQuery : IRequest<GetBeersListQueryVm> { }

    public class GetBeersListQueryValidator : AbstractValidator<GetBeersListQuery> { }

    public class GetBeersListQueryHandler : IRequestHandler<GetBeersListQuery, GetBeersListQueryVm>
    {
        private readonly IBeersDbContext _context;
        private readonly IMapper _mapper;

        public GetBeersListQueryHandler(IBeersDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetBeersListQueryVm> Handle(GetBeersListQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.Beers.ToListAsync();
            var dtos = _mapper.Map<List<BeerDto>>(entities);
            return new GetBeersListQueryVm()
            {
                Beers = dtos,
            };
        }
    }

    public class GetBeersListQueryVm 
    {
        public List<BeerDto> Beers { get; set; }
    }

    public class BeerDto
    {
        public int BeerId { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public double Price { get; set; }

        public class MapperClass : Profile
        {
            public MapperClass()
            {
                CreateMap<Beer, BeerDto>();
            }
        }
    }
}
