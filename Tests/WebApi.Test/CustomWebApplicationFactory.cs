﻿using GastosResidenciais.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace WebApi.Test;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private GastosResidenciais.Domain.Entities.User _user = default!;
    private string _password = string.Empty;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test")
            .ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<GastosResidenciaisDbContext>));
                if (descriptor is not null)
                    services.Remove(descriptor);

                var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                services.AddDbContext<GastosResidenciaisDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                    options.UseInternalServiceProvider(provider);
                });

                using var scope = services.BuildServiceProvider().CreateScope();

                var dbContext = scope.ServiceProvider.GetRequiredService<GastosResidenciaisDbContext>();

                dbContext.Database.EnsureDeleted();

                StartDatabase(dbContext);
            });
    }

    private void StartDatabase(GastosResidenciaisDbContext dbContext)
    {
        (_user, _password) = UserBuilder.Build();

        dbContext.Users.Add(_user);

        dbContext.SaveChanges();
    }
}