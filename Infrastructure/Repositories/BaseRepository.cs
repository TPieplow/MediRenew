using Infrastructure.Contexts;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    private readonly CodeFirstDbContext _context;

    protected BaseRepository(CodeFirstDbContext context)
    {
        _context = context;
    }

    public virtual async Task<TEntity> CreateAsync(TEntity entity)
    {
        try
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
        catch (Exception ex)
        { Debug.WriteLine($"ERROR : {ex.Message}"); }
        return null!;
    }

    public virtual async Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var result = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
            if (result != null)
            {
                return result;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ERROR: {ex.Message}");
        }
        return null!;
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        try
        {
            return await _context.Set<TEntity>().ToListAsync();
        }
        catch (Exception ex) { Debug.WriteLine($"ERROR : {ex.Message}"); }
        return Enumerable.Empty<TEntity>();
    }

    public virtual async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var entity = await _context.Set<TEntity>().SingleOrDefaultAsync(predicate);
            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();

                return true;
            }
            return false;
        }
        catch (Exception ex) { Debug.WriteLine($"ERROR : {ex.Message}"); }
        return false;
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        try
        {
            var entityToUpdate = _context.Set<TEntity>().Update(entity).Entity;
            await _context.SaveChangesAsync();
            return entityToUpdate;
        }
        catch (Exception ex) { Debug.WriteLine($"ERROR : {ex.Message}"); }
        return null!;
    }

    public virtual bool Exists(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            return _context.Set<TEntity>().Any(predicate);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ERROR: {ex.Message}");
            return false;
        }
    }
}
