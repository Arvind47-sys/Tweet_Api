using Microsoft.Extensions.DependencyInjection;
using Tweet_Api.Repository;
using Tweet_Api.Repository.IRepository;

namespace Tweet_Api.Extensions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>()
                    .AddScoped<ITweetRepository, TweetRepository>();

            return services;
        }
    }
}