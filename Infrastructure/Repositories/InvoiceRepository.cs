using Infrastructure.Contexts;
using Infrastructure.HospitalEntities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class InvoiceRepository(CodeFirstDbContext context) : BaseRepository<InvoiceEntity>(context)
    {
        private readonly CodeFirstDbContext _context = context;

        public override async Task<InvoiceEntity> GetOneAsync(Expression<Func<InvoiceEntity, bool>> predicate)
        {
            try
            {
                var invoice = await _context.Set<InvoiceEntity>()
                    .Include(x => x.Patient)
                    .Include(x => x.Pharmacy)
                    .FirstOrDefaultAsync(predicate);

                if (invoice is not null)
                {
                    return invoice;
                }
                else
                {
                    Debug.WriteLine("Invoice not found");
                    return null!;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR: {ex.Message}");
                return null!;
            }
        }

        public async Task<IEnumerable<InvoiceEntity>> GetAllInvoiceIncludePatientPharmacyAsync()
        {
            try
            {
                var invoice = await _context.Invoices
                    .Include(x => x.Patient)
                    .Include(x => x.Pharmacy)
                    .ToListAsync();

                return invoice;
            }
            catch (Exception ex) { Debug.WriteLine($"ERRROR: {ex.Message}"); }
            return null!;
        }

    }
}
