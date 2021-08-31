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
using Microsoft.Extensions.Configuration;

namespace Rest.Api.Features
{
    public class GetPhotosPage
    {
        public class Request: IRequest<Response>
        {
            public int PageSize { get; set; }
            public int Index { get; set; }
        }

        public class Response: ResponseBase
        {
            public int Length { get; set; }
            public List<PhotoDto> Entities { get; set; }
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
                var query = from photo in _context.Photos
                    select photo;
                
                var length = await _context.Photos.CountAsync();
                
                var photos = await query.Page(request.Index, request.PageSize)
                    .Select(x => x.ToDto(_configuration)).ToListAsync();
                
                return new()
                {
                    Length = length,
                    Entities = photos
                };
            }
            
        }
    }
}
