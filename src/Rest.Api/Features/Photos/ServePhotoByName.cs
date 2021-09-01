using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Rest.Api.Core;
using Rest.Api.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Rest.Api.Features
{
    public class ServePhotoByName
    {
        public class Request: IRequest<Response>
        {
            public string Name { get; set; }
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
                _context = context;
                _configuration = configuration;
            }
        
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                return new () {
                    Photo = (await _context.Photos.SingleOrDefaultAsync(x => x.Name == request.Name)).ToServeDto()
                };
            }
            
        }
    }
}
