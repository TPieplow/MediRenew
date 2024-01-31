using Business.DTOs;
using Business.Interfaces;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Business.Services;

public class DepartmentService(IDepartmentRepository departmentRepository) : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository = departmentRepository;

    public async Task<IEnumerable<DepartmentDTO>> GetAllDepartments()
    {
        try
        {
            var result = await _departmentRepository.GetAllAsync();

            return result.Select(x => new DepartmentDTO
            {
                Id = x.Id,
                DepartmentName = x.DepartmentName,
                HospitalId = x.HospitalId,
                Hospital = x.Hospital
            });
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ERROR : {ex.Message}");
        }
        return null!;
    }
}
