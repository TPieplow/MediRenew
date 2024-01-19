namespace Business.DTOs;

public class PatientDTO
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string City { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    public ICollection<PrescriptionDTO> Prescriptions { get; set; } = new List<PrescriptionDTO>();
    public ICollection<PharmacyDTO> Pharmacies { get; set; } = new List<PharmacyDTO>();
}


