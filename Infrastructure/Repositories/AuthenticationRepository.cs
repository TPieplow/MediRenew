﻿using Infrastructure.Contexts;
using Infrastructure.DatabaseFirstEntities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class AuthenticationRepository(DatabaseFirstDbContext dbContext) : IAuthenticationRepository
{
    private readonly DatabaseFirstDbContext _dbContext = dbContext;

    public virtual async Task<AuthenticationEntity> CreateAsync(AuthenticationEntity entity)
    {
        try
        {
            if (entity is not null)
            {
                await _dbContext.Authentications.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return entity;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ERROR: {ex.Message}");
        }
        return null!;
    }

    public virtual async Task<AuthenticationEntity> GetOneAsync(Expression<Func<AuthenticationEntity, bool>> predicate)
    {
        try
        {
            var result = await _dbContext.Authentications.FirstOrDefaultAsync(predicate);
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

    public virtual bool Exists(Expression<Func<AuthenticationEntity, bool>> predicate)
    {
        try
        {
            return _dbContext.Set<AuthenticationEntity>().Any(predicate);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ERROR: {ex.Message}");
            return false;
        }
    }
}


