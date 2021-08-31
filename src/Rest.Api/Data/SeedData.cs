using Rest.Api.Core;
using Rest.Api.Models;
using System;
using System.Linq;

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
        }
    }
}
