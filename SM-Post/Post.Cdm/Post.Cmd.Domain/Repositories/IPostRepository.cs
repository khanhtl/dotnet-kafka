using Post.Cmd.Domain.Entities;

namespace Post.Cmd.Domain.Repositories
{
    public interface IPostRepository
    {
        Task CreateAsync(PostEntity post);
        Task UpdateAsync(PostEntity post);
        Task DeleteAsync(Guid postId);
        Task<PostEntity> GetByIdAsync(Guid postId);
        Task<List<PostEntity>> GetAllAsync();
        Task<List<PostEntity>> GetByAuthorAsync(string author);
        Task<List<PostEntity>> GetWithLikesAsync(int numberOfList);
        Task<List<PostEntity>> GetWithCommentsAsync();

    }
}
