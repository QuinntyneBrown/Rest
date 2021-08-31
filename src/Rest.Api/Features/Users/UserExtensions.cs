using System;
using Rest.Api.Models;

namespace Rest.Api.Features
{
    public static class UserExtensions
    {
        public static UserDto ToDto(this User user)
        {
            return new ()
            {
                UserId = user.UserId,
                Username = user.Username
            };
        }
        
    }
}
