using Microsoft.Extensions.DependencyInjection;

namespace Messenger.WebApi
{
    /// <summary>
    /// Static classe responsible for providing actions for .NET API dependency injection. 
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Method responsible for doing the dependency injection of the notifier.
        /// </summary>
        /// <param name="services">IServiceCollection.</param>
        public static void AddMessenger(this IServiceCollection services)
        {
            services.AddScoped<INotifier, Notifier>();
        }
    }
}
