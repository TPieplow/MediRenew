namespace Business.DTOs;

public class PrescriptionDTO
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Cost { get; set; }
    public string Dosage { get; set; } = null!;
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public int PharmacyId { get; set; }

    public PharmacyDTO Pharmacy { get; set; } = null!;
}

