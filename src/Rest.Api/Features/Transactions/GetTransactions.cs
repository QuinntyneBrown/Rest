using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Rest.Api.Core;
using Rest.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Rest.Api.Features
{
    public class GetTransactions
    {
        public class Request: IRequest<Response> { }

        public class Response: ResponseBase
        {
            public List<TransactionDto> Transactions { get; set; }
        }

        public class Handler: IRequestHandler<Request, Response>
        {
            private readonly IRestDbContext _context;
        
            public Handler(IRestDbContext context)
                => _context = context;
        
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                return new () {
                    Transactions = await _context.Transactions.Select(x => x.ToDto()).ToListAsync()
                };
            }
            
        }
    }
}
