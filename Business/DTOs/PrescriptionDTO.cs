using Infrastructure.HospitalEntities;
using Microsoft.Identity.Client;

namespace Business.DTOs;

public class PrescriptionDTO
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Cost { get; set; }
    public string Dosage { get; set; } = null!;
    public string MedicationName { get; set; } = null!;
    public string DoctorName { get; set; } = null!;
    public string PatientName { get; set; } = null!;
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public int PharmacyId { get; set; }

    public DoctorEntity Doctor { get; set; } = null!;
    public PatientEntity Patient { get; set; } = null!;
    public PharmacyDTO Pharmacy { get; set; } = null!;
}

