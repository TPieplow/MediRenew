using Business.DTOs;
using Infrastructure.HospitalEntities;
using Infrastructure.Repositories;
using System.Diagnostics;
using static Infrastructure.Utils.ResultEnums;


namespace Business.Services
{
    public class InvoiceService(InvoiceRepository invoiceRepository)
    {
        private readonly InvoiceRepository _invoiceRepository = invoiceRepository;


        public async Task<Result> AddInvoiceAsync(InvoiceDTO invoice)
        {
            try
            {
                var newInvoiceEntity = new InvoiceEntity
                {
                    Id = invoice.Id,
                    Description = invoice.Description,
                    Cost = invoice.Cost,
                    TotalCost = invoice.TotalCost,
                    PatientId = invoice.PatientId,
                    PharmacyId = invoice.PharmacyId,
                    Patient = invoice.Patient,
                    Pharmacy = invoice.Pharmacy,
                };
                var result = await _invoiceRepository.CreateAsync(newInvoiceEntity);

                if (result is not null)
                {
                    return Result.Success;
                }
            }
            catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); }
            return Result.Failure;
        }

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
            catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); }
            return new List<InvoiceDTO>();
        }
    }

}
