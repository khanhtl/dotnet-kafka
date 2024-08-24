﻿using Microsoft.EntityFrameworkCore;
using Post.Query.Domain.Entities;
using Post.Query.Domain.Repositories;
using Post.Query.Infrastructure.Data;

namespace Post.Query.Infrastructure.Repositories;

internal class CommentRepository : ICommentRepository
{
    private readonly DatabaseContextFactory _contextFactory;

    public CommentRepository(DatabaseContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task CreateAsync(CommentEntity comment)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();
        context.Comments.Add(comment);
        _ = await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid commentId)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();
        var comment = await GetByIdAsync(commentId);
        if (comment == null) return;
        context.Comments.Remove(comment);
        _ = await context.SaveChangesAsync();
    }

    public Task<CommentEntity> GetByIdAsync(Guid commentId)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();
        return context.Comments.FirstOrDefaultAsync(c => c.CommentId == commentId);
    }

    public async Task UpdateAsync(CommentEntity comment)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();
        context.Update(comment);
        _ = await context.SaveChangesAsync();
    }
}
