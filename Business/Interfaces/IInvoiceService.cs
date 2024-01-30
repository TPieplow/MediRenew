using Business.DTOs;
using Infrastructure.Utils;

namespace Business.Interfaces
{
    public interface IInvoiceService
    {
        /// <summary>
        /// Adds a new invoice to the database.
        /// </summary>
        /// <param name="invoice">The InvoiceDTO containing the properties of the new invoice.</param>
        /// <returns>A Result indicating success or failure of the CRUD-operation.</returns>
        Task<ResultEnums.Result> AddInvoiceAsync(InvoiceDTO invoice);

        /// <summary>
        /// Gets one invoice based on a given patientId.
        /// </summary>
        /// <param name="patientId">The patientId used to search the database for the right invoice retreive.</param>
        /// <returns>A DTO containing the retrieved InvoideId.</returns>
        Task<InvoiceDTO> GetOneInvoice(int patientId);

        /// <summary>
        /// Removes a given invoice using invoiceId.
        /// </summary>
        /// <param name="invoiceId">The invoiceId used to search the database for the right invoice to delete</param>
        /// <returns>A Result indicating success or failure of the CRUD-operation.</returns>
        Task<ResultEnums.Result> RemoveInvoiceAsync(int invoiceId);

        /// <summary>
        /// Gets a list of all invoices stored in the database.
        /// </summary>
        /// <returns>Return a list of all invoices in the database.</returns>
        Task<IEnumerable<InvoiceDTO>> ViewPatientInvoicesAsync();
    }
}