using System.Data;
using BlogsDAL.Interfaces;
using BlogsDAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BlogsDAL;

public class UnitOfWork(
    BlogDbContext context,
    ICategoryRepository categoryRepository,
    ICommentRepository commentRepository,
    IPostRepository postRepository,
    ITagRepository tagRepository,
    IUserJwtRepository userJwtRepository)
    : IUnitOfWork
{
    private readonly BlogDbContext _context = context;
    private bool _disposed;

    public ICategoryRepository CategoryRepository { get; set; } = categoryRepository;
    public ICommentRepository CommentRepository { get; set; } = commentRepository;
    public IPostRepository PostRepository { get; set; } = postRepository;
    public ITagRepository TagRepository { get; set; } = tagRepository;
    public IUserJwtRepository UserJwtRepository { get; set; } = userJwtRepository;

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
    
    public async Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
    {
        return await _context.Database.BeginTransactionAsync(isolationLevel);
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        _disposed = true;
    }
}