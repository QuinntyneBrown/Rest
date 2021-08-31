using Rest.Api.Helpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using Rest.Api.Interfaces;
using Rest.Api.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rest.Api.Features
{
    public class UploadPhoto
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public List<System.Guid> PhotoIds { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IRestDbContext _context { get; set; }
            public IHttpContextAccessor _httpContextAccessor { get; set; }
            public Handler(IRestDbContext context, IHttpContextAccessor httpContextAccessor)
            {
                _context = context;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var httpContext = _httpContextAccessor.HttpContext;
                var defaultFormOptions = new FormOptions();
                var photos = new List<Photo>();

                if (!MultipartRequestHelper.IsMultipartContentType(httpContext.Request.ContentType))
                    throw new Exception($"Expected a multipart request, but got {httpContext.Request.ContentType}");

                var mediaTypeHeaderValue = MediaTypeHeaderValue.Parse(httpContext.Request.ContentType);

                var boundary = MultipartRequestHelper.GetBoundary(
                    mediaTypeHeaderValue,
                    defaultFormOptions.MultipartBoundaryLengthLimit);

                var reader = new MultipartReader(boundary, httpContext.Request.Body);

                var section = await reader.ReadNextSectionAsync();

                while (section != null)
                {
                    Photo photo;

                    var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out ContentDispositionHeaderValue contentDisposition);

                    if (hasContentDispositionHeader)
                    {
                        if (MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
                        {
                            using (var targetStream = new MemoryStream())
                            {
                                await section.Body.CopyToAsync(targetStream);

                                var name = $"{contentDisposition.FileName}".Trim(new char[] { '"' }).Replace("&", "and");

                                photo = _context.Photos.SingleOrDefault(x => x.Name == name);

                                if (photo == null)
                                {
                                    photo = new Photo(name);

                                    _context.Photos.Add(photo);
                                }

                                photo.Update(StreamHelper.ReadToEnd(targetStream), section.ContentType);

                            }

                            photos.Add(photo);
                        }
                    }

                    section = await reader.ReadNextSectionAsync();
                }

                await _context.SaveChangesAsync(cancellationToken);

                return new ()
                {
                    PhotoIds = photos.Select(x => x.PhotoId).ToList()
                };
            }
        }
    }
}
