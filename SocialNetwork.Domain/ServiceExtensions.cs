using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Domain.Abstractions;
using SocialNetwork.Domain.Database;
using SocialNetwork.Domain.Services;

namespace SocialNetwork.Domain
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddSocialNetworkServices(this IServiceCollection collection)
        {
            collection.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            collection.AddScoped<IUserService, UserService>();
            collection.AddScoped<IUserSubscribeService, UserSubscribeService>();

            return collection;
        }
    }
}