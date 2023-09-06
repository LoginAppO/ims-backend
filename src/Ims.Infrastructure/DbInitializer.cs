using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ims.Domain;
using Microsoft.Extensions.Configuration;

namespace Ims.Infrastructure;

public static class DbInitializer
{
    public static void Initialize(ImsDbContext context, IConfiguration configuration)
    {
        context.Database.EnsureCreated();

        if (!context.Users.Any())
        {
            var initialUser = new User
            {
                Email = configuration["InitialUser:Email"] ?? string.Empty,
                Password = BCrypt.Net.BCrypt.HashPassword(configuration["InitialUser:Password"] ?? string.Empty)
            };

            context.Users.Add(initialUser);
            context.SaveChanges();
        }
    }
}
