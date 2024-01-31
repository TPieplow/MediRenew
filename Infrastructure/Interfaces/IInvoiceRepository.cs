using Infrastructure.HospitalEntities;
using System.Linq.Expressions;

namespace Infrastructure.Interfaces
{
    public interface IInvoiceRepository : IBaseRepository<InvoiceEntity>
    {
        /// <summary>
        /// Retreives all invoices including patient pharmacy.
        /// </summary>
        /// <returns>A IEnumerable list of all invoices including patient pharmacy</returns>
        Task<IEnumerable<InvoiceEntity>> GetAllInvoiceIncludePatientPharmacyAsync();

        /// <summary>
        /// Retreives one invoice given the conditions.
        /// </summary>
        /// <param name="predicate">The given conditions.</param>
        /// <returns>True if found, otherwise false.</returns>
        new Task<InvoiceEntity> GetOneAsync(Expression<Func<InvoiceEntity, bool>> predicate);
    }
}