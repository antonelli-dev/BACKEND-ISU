using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository;

namespace WebApi.IntegrationTests.Factories;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<RepositoryContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<RepositoryContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
            });

            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<RepositoryContext>();            
            db.Database.EnsureCreated();
            SeedDatabase(db);
        });
    }

    private void SeedDatabase(RepositoryContext context)
    {
        context.Tasks.Add(new Entities.Models.TaskItem
        {
            TaskId = 1,
            Title = "Test Task",
            Description = "This is a test task",
            DueDate = DateTime.Now.AddDays(1),
            IsCompleted = false
        });

        context.SaveChanges();
    }
}
