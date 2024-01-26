using Business.DTOs;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Business.Services
{
    public class InvoiceService(InvoiceRepository invoiceRepository)
    {
        public readonly InvoiceRepository _invoiceRepository = invoiceRepository;

        public async Task<IEnumerable<InvoiceDTO>> ViewPatientInvoices()
        {

            try
            {
                var result = (await _invoiceRepository.GetAllInvoiceIncludePatientPharmacyAsync()).ToList();

                if (result.Count < 0)
                {
                    return new List<InvoiceDTO>();
                }

                return result.Select(x => new InvoiceDTO
                {
                    Id = x.Id,
                    Description = x.Description,
                    Cost = x.Cost,
                    TotalCost = x.TotalCost,
                    PatientId = x.PatientId,
                    PatientName = x.Patient.FirstName + " " + x.Patient.LastName,
                    MedicationName = x.Pharmacy.MedicationName
                });
            }
            catch (Exception ex) { Debug.WriteLine( $"ERROR: {ex.Message}"); }
            return new List<InvoiceDTO>();
        }
    }

}
