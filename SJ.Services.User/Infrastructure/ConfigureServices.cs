using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ConfigureServices
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            AddDbContext(services, configuration);
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserContext>(opt =>
            {
                var accountEndpoint = configuration["CosmoDb:AccountEndpoint"]!;
                var accountKey = configuration["CosmoDb:AccountKey"]!;
                var databaseName = configuration["CosmoDb:DatabaseName"]!;

                opt.UseCosmos(accountEndpoint, accountKey, databaseName, opt =>
                {
                    opt.ConnectionMode(Microsoft.Azure.Cosmos.ConnectionMode.Direct);
                });
            });
        }
    }
}
