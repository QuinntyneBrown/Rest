using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Rest.Api.Models;
using Rest.Api.Core;
using Rest.Api.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Rest.Api.Features
{
    public class CreatePhoto
    {
        public class Validator: AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Photo).NotNull();
                RuleFor(request => request.Photo).SetValidator(new PhotoValidator());
            }
        
        }

        public class Request: IRequest<Response>
        {
            public PhotoDto Photo { get; set; }
        }

        public class Response: ResponseBase
        {
            public PhotoDto Photo { get; set; }
        }

        public class Handler: IRequestHandler<Request, Response>
        {
            private readonly IRestDbContext _context;
            private readonly IConfiguration _configuration;
        
            public Handler(IRestDbContext context, IConfiguration configuration)
            {
                _configuration = configuration;
                _context = context;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var photo = new Photo(request.Photo.Name);
                
                _context.Photos.Add(photo);
                
                await _context.SaveChangesAsync(cancellationToken);
                
                return new ()
                {
                    Photo = photo.ToDto(_configuration)
                };
            }
            
        }
    }
}
