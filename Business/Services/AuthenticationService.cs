using Infrastructure.DatabaseFirstEntities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using System.Security.Cryptography;
using System.Text;
using static Infrastructure.Utils.ResultEnums;


namespace Business.Services
{

    public class AuthenticationService(AuthenticationRepository authenticationRepository)
    {
        private readonly AuthenticationRepository _authenticationRepository = authenticationRepository;

        public async Task<Result> CreateUserAndLoginAsync(string username, string password)
        {
            var hashedPassword = HashPassword(password);

            if (_authenticationRepository.Exists(x => x.Username == username))
            {
                return Result.Failure;
            }

            var newUser = new AuthenticationEntity
            {
                Username = username,
                PasswordHash = hashedPassword,
            };

            var registrationResult = await _authenticationRepository.CreateUserAsync(newUser);
            if(registrationResult != null)
            {
                return Result.Success;
            }
            return Result.Failure;
        }

        public async Task<bool> ValidateUserAsync(string username, string password)
        {
            var user = await _authenticationRepository.GetOneAsync(a => a.Username == username);
            var hashedPassword = HashPassword(password);

            if (user is not null)
            {
                return VerifyPassword(password, user.PasswordHash);
            }

            return false;
        }

        private static bool VerifyPassword(string enteredPassword, string storedPasswordHash)
        {
            var enteredPasswordHash = HashPassword(enteredPassword);
            return string.Equals(enteredPasswordHash, storedPasswordHash, StringComparison.OrdinalIgnoreCase);
        }

        private static string HashPassword(string password)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var hashBytes = SHA256.HashData(passwordBytes);
            var hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

            return hashString;
        }
    }
};

