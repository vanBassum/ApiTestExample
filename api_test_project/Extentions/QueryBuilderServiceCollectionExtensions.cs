using ApiExample.Queries;
using System.Reflection;

namespace PoC_API.Extentions
{
    public static class QueryBuilderServiceCollectionExtensions
    {
        public static IServiceCollection AddQueryBuilders(this IServiceCollection services, Assembly? assembly = null)
        {
            assembly ??= Assembly.GetExecutingAssembly();

            var queryBuilderInterfaceType = typeof(IQueryBuilder<,>);
            var types = assembly.GetTypes();

            foreach (var type in types)
            {
                if (type.IsClass && !type.IsAbstract)
                {
                    var implementedInterfaces = type.GetInterfaces()
                        .Where(i =>
                            i.IsGenericType &&
                            i.GetGenericTypeDefinition() == queryBuilderInterfaceType);

                    foreach (var implementedInterface in implementedInterfaces)
                    {
                        services.AddScoped(implementedInterface, type);
                    }
                }
            }

            return services;
        }
    }


}
