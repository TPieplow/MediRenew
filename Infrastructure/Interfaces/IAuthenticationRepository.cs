using Infrastructure.DatabaseFirstEntities;
using Infrastructure.HospitalEntities;
using System.Linq.Expressions;

namespace Infrastructure.Interfaces
{
    public interface IAuthenticationRepository
    {
        Task<AuthenticationEntity> CreateAsync(AuthenticationEntity entity);
        bool Exists(Expression<Func<AuthenticationEntity, bool>> predicate);
        Task<AuthenticationEntity> GetOneAsync(Expression<Func<AuthenticationEntity, bool>> predicate);
    }
}