using Microsoft.EntityFrameworkCore;

namespace Weldify.Data
{
    public class DatabaseInitializer
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public DatabaseInitializer(
            IDbContextFactory<AppDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task InitializeAsync()
        {
            await using var db = await _contextFactory.CreateDbContextAsync().ConfigureAwait(false);

            await db.Database.EnsureCreatedAsync().ConfigureAwait(false);
        }
    }
}
