using Infrastructure.HospitalEntities;
using System.Linq.Expressions;

namespace Infrastructure.Interfaces
{
    public interface IInvoiceRepository : IBaseRepository<InvoiceEntity>
    {
        Task<IEnumerable<InvoiceEntity>> GetAllInvoiceIncludePatientPharmacyAsync();
        new Task<InvoiceEntity> GetOneAsync(Expression<Func<InvoiceEntity, bool>> predicate);
    }
}