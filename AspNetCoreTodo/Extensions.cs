using System;
using AspNetCoreTodo.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class Extensions
{
    public static IWebHost MigrateDatabase(this IWebHost webHost)
    {
        // Manually run any pending migrations if configured to do so.
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        if (env == "Production")
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<ApplicationDbContext>();

                dbContext.Database.Migrate();
            }
        }

        return webHost;
    }
}