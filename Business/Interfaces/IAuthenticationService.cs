using Infrastructure.Utils;

namespace Business.Interfaces
{
    public interface IAuthenticationService
    {
        Task<ResultEnums.Result> CreateUserAndLoginAsync(string username, string password);
        Task<bool> ValidateUserAsync(string username, string password);
    }
}