using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineStore.Persistance.Context;

namespace OnlineStore.IntegrationTest.Factories;
public class WebAppDbContextFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbContextOptions<OnlineStoreDbContext>));
            if (descriptor != null)
                services.Remove(descriptor);

            services.AddDbContext<OnlineStoreDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryTest");
            });

            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            using var appContext = scope.ServiceProvider.GetRequiredService<OnlineStoreDbContext>();
            appContext.Database.EnsureCreated();
        });
    }

}