using System.Linq.Expressions;

namespace Infrastructure.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Adds an entity to the database
        /// </summary>
        /// <param name="entity">Entity to be added</param>
        /// <returns>Entity</returns>
        Task<TEntity> CreateAsync(TEntity entity);

        /// <summary>
        /// Retrieves one entity from the database
        /// </summary>
        /// <param name="predicate">Conditions given</param>
        /// <returns>Found entity if true, else null</returns>
        Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Retrieves all entities from a given table in database
        /// </summary>
        /// <returns>IEnum of entities</returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Updates one entity in database
        /// </summary>
        /// <param name="entity">Entity to be updated</param>
        /// <returns>Updated entity</returns>
        Task<TEntity> UpdateAsync(TEntity entity); 

        /// <summary>
        /// Deletes one entity in database
        /// </summary>
        /// <param name="predicate">Conditions given</param>
        /// <returns>True if entity found else false</returns>
        Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Checks if anything exists given the conditions
        /// </summary>
        /// <param name="predicate">Conditions given</param>
        /// <returns>True if conditions are met on elements else false</returns>
        bool Exists(Expression<Func<TEntity, bool>> predicate);
    }
}