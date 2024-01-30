using Business.DTOs;
using Infrastructure.Utils;

namespace Business.Interfaces
{
    public interface IInvoiceService
    {
        Task<ResultEnums.Result> AddInvoiceAsync(InvoiceDTO invoice);
        Task<InvoiceDTO> GetOneInvoice(int patientId);
        Task<ResultEnums.Result> RemoveInvoiceAsync(int invoiceId);
        Task<IEnumerable<InvoiceDTO>> ViewPatientInvoicesAsync();
    }
}