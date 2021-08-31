using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Rest.Api.Models;
using Rest.Api.Core;
using Rest.Api.Interfaces;

namespace Rest.Api.Features
{
    public class CreateTransaction
    {
        public class Validator: AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Transaction).NotNull();
                RuleFor(request => request.Transaction).SetValidator(new TransactionValidator());
            }
        
        }

        public class Request: IRequest<Response>
        {
            public TransactionDto Transaction { get; set; }
        }

        public class Response: ResponseBase
        {
            public TransactionDto Transaction { get; set; }
        }

        public class Handler: IRequestHandler<Request, Response>
        {
            private readonly IRestDbContext _context;
        
            public Handler(IRestDbContext context)
                => _context = context;
        
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var transaction = new Transaction();
                
                _context.Transactions.Add(transaction);
                
                await _context.SaveChangesAsync(cancellationToken);
                
                return new Response()
                {
                    Transaction = transaction.ToDto()
                };
            }
            
        }
    }
}
