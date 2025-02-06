using System.Data;
using BlogsDAL.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace BlogsDAL.Interfaces;

public interface IUnitOfWork : IDisposable
{
    ICategoryRepository CategoryRepository { get; set; }
    ICommentRepository CommentRepository { get; set; }
    IPostRepository PostRepository { get; set; }
    ITagRepository TagRepository { get; set; }
    IUserJwtRepository UserJwtRepository { get; set; }
    IUserFollowerRepository UserFollowerRepository { get; set; }
    
    Task<int> SaveAsync();
    
    Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
}