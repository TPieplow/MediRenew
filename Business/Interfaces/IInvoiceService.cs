using Business.DTOs;
using Infrastructure.Utils;

namespace Business.Interfaces
{
    public interface IInvoiceService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        Task<ResultEnums.Result> AddInvoiceAsync(InvoiceDTO invoice);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        Task<InvoiceDTO> GetOneInvoice(int patientId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        Task<ResultEnums.Result> RemoveInvoiceAsync(int invoiceId);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<InvoiceDTO>> ViewPatientInvoicesAsync();
    }
}