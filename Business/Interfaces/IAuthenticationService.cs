using Infrastructure.Utils;

namespace Business.Interfaces
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<ResultEnums.Result> CreateUserAndLoginAsync(string username, string password);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<bool> ValidateUserAsync(string username, string password);
    }
}