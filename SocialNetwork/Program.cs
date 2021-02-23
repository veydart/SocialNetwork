using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SocialNetwork.Domain.Database;

namespace SocialNetwork
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().MigrateData().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

    public static class ProgramExtensions
    {
        public static IHost MigrateData(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            scope.ServiceProvider.GetRequiredService<SocialNetworkDbContext>().Database.Migrate();

            return host;
        }
    }
}