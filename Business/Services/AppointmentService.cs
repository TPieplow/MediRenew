using Business.DTOs;
using Infrastructure.HospitalEntities;
using Infrastructure.Repositories;
using System.Diagnostics;
using static Infrastructure.Utils.ResultEnums;

namespace Business.Services;

public class AppointmentService
{
    private readonly AppointmentRepository _appointmentRepository;

    public AppointmentService(AppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    public async Task<Result> AddApointment(AppointmentDTO newAppointment)
    {
        try
        {
            if(_appointmentRepository.Exists(x => x.PatientId == newAppointment.PatientId))
            {
                return Result.Failure;
            }

            var newAppointmentEntity = new AppointmentEntity
            {
                PatientId = newAppointment.PatientId,
                DoctorId = newAppointment.DoctorId,
                Date = newAppointment.Date,
                Comments = newAppointment.Comments
            };
            var result = await _appointmentRepository.CreateAsync(newAppointmentEntity);
            if(result is not null)
            {
                return Result.Success;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ERROR : {ex.Message}");
        }
        return Result.Failure;
    }

    public async Task<AppointmentDTO> GetOneAppointment(int id)
    {
        try
        {
            var appointment = await _appointmentRepository.GetOneAsync(x => x.PatientId == id);

            if(appointment is null)
            {
                return null!;
            }

            var appointmentDTO = new AppointmentDTO
            {
                PatientId = appointment.PatientId,
                DoctorId = appointment.DoctorId,
                PatientName = appointment.Patient.FirstName + " " + appointment.Patient.LastName,
                DoctorName = appointment.Doctor.FirstName + " " + appointment.Doctor.LastName,
                Date = appointment.Date,
                Comments = appointment.Comments
            };
            return appointmentDTO;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ERROR : {ex.Message}");
        }
        return null!;
    }

    public async Task <IEnumerable<AppointmentDTO>> GetAllAppointments()
    {
        try
        {
            var result = (await _appointmentRepository.GetAllAsync()).ToList();

            return result.Select(x => new AppointmentDTO
            {
                PatientId = x.PatientId,
                DoctorId = x.DoctorId,
                PatientName = x.Patient.FirstName + " " + x.Patient.LastName,
                DoctorName = x.Doctor.FirstName + " " + x.Doctor.LastName,
                Date = x.Date,
                Comments = x.Comments
            });
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ERROR : {ex.Message}");
        }
        return null!;
    }
}
