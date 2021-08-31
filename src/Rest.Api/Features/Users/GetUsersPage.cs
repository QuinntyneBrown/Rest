using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Rest.Api.Extensions;
using Rest.Api.Core;
using Rest.Api.Interfaces;
using Rest.Api.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Rest.Api.Features
{
    public class GetUsersPage
    {
        public class Request: IRequest<Response>
        {
            public int PageSize { get; set; }
            public int Index { get; set; }
        }

        public class Response: ResponseBase
        {
            public int Length { get; set; }
            public List<UserDto> Entities { get; set; }
        }

        public class Handler: IRequestHandler<Request, Response>
        {
            private readonly IRestDbContext _context;
        
            public Handler(IRestDbContext context)
                => _context = context;
        
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var query = from user in _context.Users
                    select user;
                
                var length = await _context.Users.CountAsync();
                
                var users = await query.Page(request.Index, request.PageSize)
                    .Select(x => x.ToDto()).ToListAsync();
                
                return new()
                {
                    Length = length,
                    Entities = users
                };
            }
            
        }
    }
}
