using Infrastructure.DatabaseFirstEntities;
using Infrastructure.HospitalEntities;
using System.Linq.Expressions;

namespace Infrastructure.Interfaces
{
    public interface IAuthenticationRepository : IBaseRepository<AuthenticationEntity>
    {
        new Task<AuthenticationEntity> CreateAsync(AuthenticationEntity entity);
        new bool Exists(Expression<Func<AuthenticationEntity, bool>> predicate);
        new Task<AuthenticationEntity> GetOneAsync(Expression<Func<AuthenticationEntity, bool>> predicate);
    }
}