using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using Rest.Api.Models;
using Rest.Api.Core;
using Rest.Api.Interfaces;

namespace Rest.Api.Features
{
    public class RemoveTransaction
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
                var transaction = await _context.Transactions.SingleAsync(x => x.TransactionId == request.TransactionId);
                
                _context.Transactions.Remove(transaction);
                
                await _context.SaveChangesAsync(cancellationToken);
                
                return new Response()
                {
                    Transaction = transaction.ToDto()
                };
            }
            
        }
    }
}
