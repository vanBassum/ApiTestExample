using System.Reflection;

namespace ApiExample.Infrastructure.Mapping
{
    public static class MapperServiceCollectionExtensions
    {
        public static IServiceCollection AddMappers(this IServiceCollection services, Assembly? assembly = null)
        {
            assembly ??= Assembly.GetExecutingAssembly();

            var mapperInterfaceType = typeof(IMapper<,>);
            var types = assembly.GetTypes();

            foreach (var type in types)
            {
                if (type.IsClass && !type.IsAbstract)
                {
                    var implementedInterface = type.GetInterfaces()
                        .FirstOrDefault(i =>
                            i.IsGenericType &&
                            i.GetGenericTypeDefinition() == mapperInterfaceType);

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
