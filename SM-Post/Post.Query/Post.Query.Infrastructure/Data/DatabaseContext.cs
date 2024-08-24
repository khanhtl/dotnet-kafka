using Microsoft.EntityFrameworkCore;
using Post.Query.Domain.Entities;

namespace Post.Query.Infrastructure.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<PostEntity> Posts { get; set; }
    public DbSet<CommentEntity> Comments{ get; set; }

}
