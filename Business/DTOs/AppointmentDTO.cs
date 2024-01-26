using Infrastructure.HospitalEntities;


namespace Business.DTOs;

public class AppointmentDTO
{
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public string DoctorName { get; set; } = null!;
    public string PatientName { get; set; } = null!;
    public DateTime Date { get; set; }
    public string? Comments { get; set; }

    public virtual PatientEntity Patient { get; set; } = null!;
    public virtual DoctorEntity Doctor { get; set; } = null!;
}
