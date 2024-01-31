using Infrastructure.DatabaseFirstEntities;
using Infrastructure.HospitalEntities;
using System.Linq.Expressions;

namespace Infrastructure.Interfaces
{
    public interface IAuthenticationRepository
    {
        /// <summary>
        /// Creates a new user in the authentications table using the authentications entity.
        /// </summary>
        /// <param name="entity">The entity to be inserted into the database.</param>
        /// <returns>Returns the entity</returns>
        Task<AuthenticationEntity> CreateAsync(AuthenticationEntity entity);

        /// <summary>
        /// Checks if any record in the AuthenticationEntity table satesfies the given condition
        /// </summary>
        /// <param name="predicate">The condition to check for.</param>
        /// <returns>True if found, otherwise false.</returns>
        bool Exists(Expression<Func<AuthenticationEntity, bool>> predicate);

        /// <summary>
        /// Checks if any record in the AuthenticationEntity table satesfies the given condition
        /// </summary>
        /// <param name="predicate">The condition to check for.</param>
        /// <returns>True if found, otherwise false.</returns>
        Task<AuthenticationEntity> GetOneAsync(Expression<Func<AuthenticationEntity, bool>> predicate);
    }
}