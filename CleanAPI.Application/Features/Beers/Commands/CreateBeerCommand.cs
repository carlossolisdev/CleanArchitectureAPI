using AutoMapper;
using CleanAPI.Application.Interfaces.Persistence;
using CleanAPI.Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CleanAPI.Application.Features.Beers.Commands
{
    public class CreateBeerCommand : IRequest<CreateBeerCommandVm>
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public double Price { get; set; }

        public class MapperClass : Profile
        {
            public MapperClass()
            {
                CreateMap<CreateBeerCommand, Beer>();
            }
        }
    }

    public class CreateBeerCommandValidator : AbstractValidator<CreateBeerCommand>
    {
        public CreateBeerCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Name no puede estar vacio");

            RuleFor(x => x.Brand)
                .NotEmpty()
                .NotNull()
                .WithMessage("Brand no puede estar vacio");

            RuleFor(x => x.Price)
                .NotEmpty()
                .NotNull()
                .WithMessage("Price no puede estar vacio")
                .GreaterThan(1.0)
                .WithMessage("Price debe ser mayor a 1");
        }
    }

    public class CreateBeerCommandHandler : IRequestHandler<CreateBeerCommand, CreateBeerCommandVm>
    {
        private readonly IBeersDbContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateBeerCommand> _validator;

        public CreateBeerCommandHandler(IBeersDbContext context, IMapper mapper, IValidator<CreateBeerCommand> validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<CreateBeerCommandVm> Handle(CreateBeerCommand request, CancellationToken cancellationToken)
        {
            // TODO: Extract to a common method
            ValidationResult result = await _validator.ValidateAsync(request);
            if (!result.IsValid) {
                string errors = "";
                result.Errors.ForEach(x => errors += $"{x.ErrorMessage} ");
                throw new Exception(errors);
            }

            var entity = _mapper.Map<Beer>(request);
            _context.Beers.Add(entity);
            var vm = _mapper.Map<CreateBeerCommandVm>(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return vm;
        }
    }

    public class CreateBeerCommandVm
    {
        public int BeerId { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public double Price { get; set; }

        public class MapperClass : Profile
        {
            public MapperClass()
            {
                CreateMap<Beer, CreateBeerCommandVm>();
            }
        }
    }
}
