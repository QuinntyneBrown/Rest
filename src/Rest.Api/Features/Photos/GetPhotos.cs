using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Rest.Api.Core;
using Rest.Api.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Rest.Api.Features
{
    public class GetPhotos
    {
        public class Request: IRequest<Response> { }

        public class Response: ResponseBase
        {
            public List<PhotoDto> Photos { get; set; }
        }

        public class Handler: IRequestHandler<Request, Response>
        {
            private readonly IRestDbContext _context;
            private readonly IConfiguration _configuration;

            public Handler(IRestDbContext context, IConfiguration configuration)
            {
                _context = context;
                _configuration = configuration;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                return new () {
                    Photos = await _context.Photos.Select(x => x.ToDto(_configuration)).ToListAsync()
                };
            }
            
        }
    }
}
