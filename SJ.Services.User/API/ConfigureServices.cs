using API.Services;

namespace API
{
    public static class ConfigureServices
    {
        public static void AddApiServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(UserService));
            services.AddScoped(typeof(CryptoService));
        }
    }
}
