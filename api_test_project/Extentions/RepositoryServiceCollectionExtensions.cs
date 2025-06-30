using ApiExample.Stores;
using System.Reflection;

namespace PoC_API.Extentions
{
    public static class StoreServiceCollectionExtensions
    {
        public static IServiceCollection AddStores(this IServiceCollection services, Assembly? assembly = null)
        {
            assembly ??= Assembly.GetExecutingAssembly();

            var repoInterfaceType = typeof(IStore<,>);
            var types = assembly.GetTypes();

            foreach (var type in types)
            {
                if (type.IsClass && !type.IsAbstract)
                {
                    var implementedInterface = type.GetInterfaces()
                        .FirstOrDefault(i =>
                            i.IsGenericType &&
                            i.GetGenericTypeDefinition() == repoInterfaceType);

                    if (implementedInterface != null)
                    {
                        services.AddScoped(implementedInterface, type);
                    }
                }
            }

            return services;
        }
    }




}
