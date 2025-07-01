using ApiExample.Data;

namespace ApiExample.Infrastructure.Seeding
{
    public interface ISeeder
    {
        void Seed(AppDbContext context);
    }
}
