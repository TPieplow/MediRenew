using Business.DTOs;
using Infrastructure.HospitalEntities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using System.Diagnostics;
using static Infrastructure.Utils.ResultEnums;


namespace Business.Services
{
    public class InvoiceService(IInvoiceRepository invoiceRepository)
    {
        private readonly IInvoiceRepository _invoiceRepository = invoiceRepository;

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

        public async Task<InvoiceDTO> GetOneInvoice(int patientId)
        {
            try
            {
                var invoiceEntity = await _invoiceRepository.GetOneAsync(x => x.Id == patientId);

                if (invoiceEntity is null)
                {
                    return null!;
                }

                var invoiceDTO = new InvoiceDTO
                {
                    Id = invoiceEntity.Id,
                    Description = invoiceEntity.Description,
                    Cost = invoiceEntity.Cost,
                    TotalCost = invoiceEntity.TotalCost,
                    PatientId = invoiceEntity.PatientId,
                    PharmacyId = invoiceEntity.PharmacyId,
                    MedicationName = invoiceEntity.Pharmacy.MedicationName,
                    Patient = invoiceEntity.Patient,
                    Pharmacy = invoiceEntity.Pharmacy
                };
                return invoiceDTO;
            }
            catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); }
            return null!;
        }

        public async Task<IEnumerable<InvoiceDTO>> ViewPatientInvoicesAsync()
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

        public async Task<Result> RemoveInvoiceAsync(int invoiceId)
        {
            try
            {
                var deleteInvoice = await _invoiceRepository.DeleteAsync(x => x.Id == invoiceId);
                if (deleteInvoice)
                {
                    return Result.Success;
                }
                else { return Result.Failure; }
            }
            catch (Exception ex)
            { Console.WriteLine($"ERROR: {ex.Message}"); return Result.Failure; }
        }
    }
}
