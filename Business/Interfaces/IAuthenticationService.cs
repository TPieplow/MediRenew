using Infrastructure.Utils;

namespace Business.Interfaces
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// Checks if the user exists, if not, creates a new AuthenticationEntity and saves if to the database.
        /// </summary>
        /// <param name="username">Input provided by user (username)</param>
        /// <param name="password">Input provided by user (password)</param>
        /// <returns></returns>
        Task<ResultEnums.Result> CreateUserAndLoginAsync(string username, string password);

        /// <summary>
        /// Validates the user by using the GetOneAsync-method and comparing the username with the stored username.
        /// Also validates the password by hashing and comparing using the HashPassword-method
        /// </summary>
        /// <param name="username">Input provided by user (username)</param>
        /// <param name="password">Input provided by user (password)</param>
        /// <returns>Returns true if validated, otherwise false</returns>
        Task<bool> ValidateUserAsync(string username, string password);
    }
}