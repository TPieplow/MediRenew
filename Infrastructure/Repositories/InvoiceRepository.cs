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
