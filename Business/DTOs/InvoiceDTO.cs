using Infrastructure.HospitalEntities;

namespace Business.DTOs
{
    public class InvoiceDTO
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public decimal Cost { get; set; }
        public decimal TotalCost { get; set; }
        public int PatientId { get; set; }
        public int PharmacyId { get; set; }
        public string PatientName { get; set; } = null!;
        public string MedicationName { get; set; } = null!;
        public virtual PatientEntity Patient { get; set; } = null!;
        public virtual PharmacyEntity Pharmacy { get; set; } = null!;
        public ICollection<PharmacyDTO> Pharmacies { get; set; } = new List<PharmacyDTO>();
    }
}
