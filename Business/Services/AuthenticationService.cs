//using Microsoft.Data.SqlClient;
//using System.Security.Cryptography;
//using System.Text;


//namespace Business.Services
//{

//    public class AuthenticationService(string connectionString)
//    {
//        private readonly string _connectionString = connectionString;

//        public bool ValidateUser(string username, string password)
//        {
//            using (var connection = new SqlConnection(_connectionString))
//            {
//                connection.Open();
//                using (var command = new SqlCommand(
//                    "SELECT PasswordHash FROM Authentications WHERE Username = @Username",
//                    connection))
//                {
//                    command.Parameters.AddWithValue("@Username", username);

//                    using (var reader = command.ExecuteReader())
//                    {
//                        if (reader.Read())
//                        {
//                            var hashedPasswordFromDatabase = reader["PasswordHash"].ToString();

//                            if (hashedPasswordFromDatabase is null)
//                                return false;
//                            return VerifyPassword(password, hashedPasswordFromDatabase);
//                        }
//                    }
//                }
//            }
//            return false;
//        }



//public bool RegisterUser(string username, string password)
//{
//    using (var connection = new SqlConnection(_connectionString))
//    {
//        connection.Open();

//        var hashedPassword = HashPassword(password);

//        using (var command = new SqlCommand(
//            "INSERT INTO Authentication (Username, PasswordHash) VALUES (@Username, @PasswordHash)",
//            connection))
//        {
//            command.Parameters.AddWithValue("@Username", username);
//            command.Parameters.AddWithValue("@PasswordHash", hashedPassword);

//            return command.ExecuteNonQuery() > 0;
//        }
//    }
//}

//        private bool VerifyPassword(string enteredPassword, string storedPasswordHash)
//        {
//            var enteredPasswordHash = HashPassword(enteredPassword);
//            return string.Equals(enteredPasswordHash, storedPasswordHash, StringComparison.OrdinalIgnoreCase);
//        }

//private string HashPassword(string password)
//{
//    var passwordBytes = Encoding.UTF8.GetBytes(password);
//    var hashBytes = SHA256.HashData(passwordBytes);
//    var hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

//    return hashString;
//}
//    }
//}
