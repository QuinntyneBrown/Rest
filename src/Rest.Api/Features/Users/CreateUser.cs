using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Rest.Api.Models;
using Rest.Api.Core;
using Rest.Api.Interfaces;

namespace Rest.Api.Features
{
    public class CreateUser
    {
        public class Validator: AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.User).NotNull();
                RuleFor(request => request.User).SetValidator(new UserValidator());
            }
        
        }

        public class Request: IRequest<Response>
        {
            public UserDto User { get; set; }
        }

        public class Response: ResponseBase
        {
            public UserDto User { get; set; }
        }

        public class Handler: IRequestHandler<Request, Response>
        {
            private readonly IRestDbContext _context;
            private readonly IPasswordHasher _passwordHasher;
        
            public Handler(IRestDbContext context, IPasswordHasher passwordHasher)
            {
                _context = context;
                _passwordHasher = passwordHasher;
            }
        
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var user = new User(request.User.Username, request.User.Password, _passwordHasher);
                
                _context.Users.Add(user);
                
                await _context.SaveChangesAsync(cancellationToken);
                
                return new ()
                {
                    User = user.ToDto()
                };
            }
            
        }
    }
}
