using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Rest.Api.Core;
using Rest.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Rest.Api.Features
{
    public class GetTransactionById
    {
        public class Request: IRequest<Response>
        {
            public Guid TransactionId { get; set; }
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
                return new () {
                    Transaction = (await _context.Transactions.SingleOrDefaultAsync(x => x.TransactionId == request.TransactionId)).ToDto()
                };
            }
            
        }
    }
}
