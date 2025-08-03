using Microsoft.Extensions.DependencyInjection;

namespace Gnobbi.DebugTools.Decorator
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection Decorate<TService>(
            this IServiceCollection services,
            Func<TService, IServiceProvider, TService> decorator)
            where TService : class
        {
            var descriptor = services.FirstOrDefault(s => s.ServiceType == typeof(TService));
            if (descriptor == null)
                throw new InvalidOperationException($"Service type {typeof(TService).Name} is not registered.");

            services.Remove(descriptor);

            services.Add(new ServiceDescriptor(
                typeof(TService),
                provider =>
                {
                    var original = (TService)(descriptor.ImplementationInstance ??
                                               descriptor.ImplementationFactory?.Invoke(provider) ??
                                               ActivatorUtilities.CreateInstance(provider, descriptor.ImplementationType));
                    return decorator(original, provider);
                },
                descriptor.Lifetime
            ));

            return services;
        }
    }
}
