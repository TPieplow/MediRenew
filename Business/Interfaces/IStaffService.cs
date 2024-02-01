using Business.DTOs;
using Infrastructure.Utils;

namespace Business.Interfaces
{
    public interface IStaffService
    {
        /// <summary>
        /// Adds a new staff member to the database.
        /// </summary>
        /// <param name="staff">The staffDTO to be saved in the database.</param>
        /// <returns>A result indicating success or failure of the CRUD-operation.</returns>
        Task<ResultEnums.Result> AddStaffMember(StaffDTO staff);

        /// <summary>
        /// Deletes a certain staff member given the Id.
        /// </summary>
        /// <param name="staffId">The Id used to search for the staff member in the database.</param>
        /// <returns>A result indicating success or failure of the CRUD-operation.</returns>
        Task<ResultEnums.Result> DeleteStaffMember(int staffId);

        /// <summary>
        /// Retreives all staff members stored in the database.
        /// </summary>
        /// <returns>A IEnumerable-list containing all staff members.</returns>
        Task<IEnumerable<StaffDTO>> GetAllStaff();

        /// <summary>
        /// Retreives on staff member stored in the database.
        /// </summary>
        /// <param name="staffId">The Id used to search for the staff member in the database.</param>
        /// <returns>Returns the DTO containing the staff member. </returns>
        Task<StaffDTO> GetOneStaffMember(int staffId);

        /// <summary>
        /// Updates a given staff member using departmentId.
        /// </summary>
        /// <param name="staff">The staff member to be updated.</param>
        /// <returns>A result indicating success or failure of the CRUD-operation.</returns>
        Task<ResultEnums.Result> UpdateStaffAsync(StaffDTO staff);
    }
}