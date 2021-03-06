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
    public class GetUsers
    {
        public class Request: IRequest<Response> { }

        public class Response: ResponseBase
        {
            public List<UserDto> Users { get; set; }
        }

        public class Handler: IRequestHandler<Request, Response>
        {
            private readonly IRestDbContext _context;
        
            public Handler(IRestDbContext context)
                => _context = context;
        
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                return new () {
                    Users = await _context.Users.Select(x => x.ToDto()).ToListAsync()
                };
            }
            
        }
    }
}
