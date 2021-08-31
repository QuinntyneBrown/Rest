using Rest.Api.Core;
using Rest.Api.Models;
using System;
using System.Linq;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Drawing;

namespace Rest.Api.Data
{
    public static class SeedData
    {
        public static void Seed(RestDbContext context)
        {
            var passwordHasher = new PasswordHasher();

            var user = new User("Quinntyne","password", passwordHasher);

            if(context.Users.SingleOrDefault(x => x.Username == user.Username) == null)
            {
                context.Users.Add(user);

                context.SaveChanges();
            }

            var provider = new FileExtensionContentTypeProvider();

            var directory = $"{System.Environment.CurrentDirectory}\\Data\\Seed\\Images";

            foreach (var path in System.IO.Directory.GetFiles(directory))
            {
                var name = System.IO.Path.GetFileName(path);

                if(context.Photos.SingleOrDefault(x => x.Name == name) == null)
                {
                    provider.TryGetContentType(name, out string contentType);

                    var photo = new Photo(name);

                    var bytes = StaticFileLocator.Get(name);

                    using(var image = Image.FromStream(new MemoryStream(bytes)))
                    {
                        photo.Update(image.PhysicalDimension.Height, image.PhysicalDimension.Width);
                    }

                    photo.Update(bytes, contentType);

                    context.Photos.Add(photo);

                    context.SaveChanges();
                }
            }
        }
    }
}
