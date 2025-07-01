using ApiExample.Data;
using System.Reflection;
using ApiExample.Infrastructure.Seeding;

namespace ApiExample.Infrastructure.Extensions
{
    public static class SeederExtensions
    {
        public static void SeedAll(this AppDbContext context, Assembly? assembly = null)
        {
            assembly ??= Assembly.GetExecutingAssembly();

            var seederType = typeof(ISeeder);
            var seederInstances = assembly.GetTypes()
                .Where(t => seederType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .Select(Activator.CreateInstance)
                .OfType<ISeeder>()
                .ToList();

            foreach (var seeder in seederInstances)
            {
                seeder.Seed(context);
            }

            context.SaveChanges(); // Persist all changes
        }
    }


}
