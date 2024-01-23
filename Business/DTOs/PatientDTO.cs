namespace Business.DTOs;

public class PatientDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string City { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Dosage { get; set; }
    public string? MedicationName { get; set; }
    public ICollection<PrescriptionDTO> Prescriptions { get; set; } = new List<PrescriptionDTO>();
}


