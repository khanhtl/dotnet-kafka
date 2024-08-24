using Microsoft.EntityFrameworkCore;
using Post.Query.Domain.Entities;
using Post.Query.Domain.Repositories;
using Post.Query.Infrastructure.Data;
using ZstdSharp.Unsafe;

namespace Post.Query.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly DatabaseContextFactory _contextFactory;

        public PostRepository(DatabaseContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public async Task CreateAsync(PostEntity post)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            context.Posts.Add(post);
            _ = await context.SaveChangesAsync();
            
        }

        public async Task DeleteAsync(Guid postId)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            var post = await GetByIdAsync(postId);
            if (post == null) return;
            context.Posts.Remove(post);
            _ = await context.SaveChangesAsync();
        }

        public async Task<List<PostEntity>> GetAllAsync()
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            return await context.Posts.Include(p => p.Commments).AsNoTracking().ToListAsync();
        }

        public Task<List<PostEntity>> GetByAuthorAsync(string author)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            return context.Posts.Include(p => p.Commments).AsNoTracking().Where(p => p.Author.Contains(author)).ToListAsync();
        }

        public Task<PostEntity> GetByIdAsync(Guid postId)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            return context.Posts.Include(p => p.Commments).FirstOrDefaultAsync(p => p.PostId == postId);
        }

        public Task<List<PostEntity>> GetWithCommentsAsync()
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            return context.Posts.AsNoTracking().Include(p => p.Commments).Where(x => x.Commments != null && x.Commments.Any()).ToListAsync();
        }

        public Task<List<PostEntity>> GetWithLikesAsync(int numberOfLike)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            return context.Posts.AsNoTracking().Include(p => p.Commments).Where(x => x.Likes >= numberOfLike).ToListAsync();
        }

        public async Task UpdateAsync(PostEntity post)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            context.Posts.Update(post);
            _ = await context.SaveChangesAsync();
        }
    }
}
