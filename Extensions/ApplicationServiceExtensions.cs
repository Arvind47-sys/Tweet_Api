using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tweet_Api.Data;
using Tweet_Api.Interfaces;
using Tweet_Api.Mapper;
using Tweet_Api.Services;

namespace Tweet_Api.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DataContext>(options =>
            {
                options.UseInMemoryDatabase("TweetAppDB");
            });
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.AddScoped<ITokenService, TokenService>()
                    .AddScoped<ITweetAppService, TweetAppService>();

            return services;
        }
    }
}