using Microsoft.EntityFrameworkCore;

namespace Post.Query.Infrastructure.Data;

public class DatabaseContextFactory
{
    private readonly Action<DbContextOptionsBuilder> _dbContextOptionsBuilder;

    public DatabaseContextFactory(Action<DbContextOptionsBuilder> dbContextOptionsBuilder)
    {
        _dbContextOptionsBuilder = dbContextOptionsBuilder;
    }

    public DatabaseContext CreateDbContext()
    {
        DbContextOptionsBuilder<DatabaseContext> optionsBuilder = new();
        _dbContextOptionsBuilder(optionsBuilder);
        return new DatabaseContext(optionsBuilder.Options);
    }
}
