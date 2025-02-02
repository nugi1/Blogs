using System.Linq.Expressions;
using BlogsDAL.Interfaces;
using BlogsDAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Shared.Exceptions;

namespace BlogsDAL.Repositories;

public class GenericRepository<T>(BlogDbContext context) : IGenericRepository<T>
    where T : Base
{
    protected DbSet<T> DbSet { get; } = context.Set<T>();

    public async Task<T> GetByIdAsync(object id)
    {
        var entity = await DbSet.FindAsync(id);
        return entity ?? throw new NotFoundException($"Entity with id {id} does not exist!");
    }

    public async Task<IList<T>> GetAsync(
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object?>>? include = null,
        bool noTracking = true)
    {
        IQueryable<T> query = DbSet;
        query = noTracking ? query.AsNoTracking() : query;
        query = predicate != null ? query.Where(predicate) : query;
        query = include != null ? include(query) : query;

        return await (orderBy != null ? orderBy(query).ToListAsync() : query.ToListAsync());
    }

    public async Task<T> GetOneAsync(
        Expression<Func<T, bool>>? predicate,
        Func<IQueryable<T>, IIncludableQueryable<T, object?>>? include = null,
        bool noTracking = true)
    {
        var query = await GetAsync(predicate: predicate, include: include, noTracking: noTracking);
        var entity = query.FirstOrDefault();
        return entity ?? throw new NotFoundException("Entity does not exist!");
    }

    public async Task AddAsync(T entity)
    {
        await DbSet.AddAsync(entity);
    }

    public async Task DeleteAsync(object id)
    {
        var entityToDelete = await DbSet.FindAsync(id);
        if (entityToDelete != null)
        {
            DbSet.Remove(entityToDelete);
        }
    }

    public void Update(T entity)
    {
        if (DbSet.Entry(entity).State == EntityState.Detached)
        {
            DbSet.Attach(entity);
        }

        DbSet.Entry(entity).State = EntityState.Modified;
    }

    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> condition)
    {
        return await DbSet.AnyAsync(condition);
    }
}