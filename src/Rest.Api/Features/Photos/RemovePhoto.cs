using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using Rest.Api.Models;
using Rest.Api.Core;
using Rest.Api.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Rest.Api.Features
{
    public class RemovePhoto
    {
        public class Request: IRequest<Response>
        {
            public Guid PhotoId { get; set; }
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
                var photo = await _context.Photos.SingleAsync(x => x.PhotoId == request.PhotoId);
                
                _context.Photos.Remove(photo);
                
                await _context.SaveChangesAsync(cancellationToken);
                
                return new ()
                {
                    Photo = photo.ToDto(_configuration)
                };
            }
            
        }
    }
}
