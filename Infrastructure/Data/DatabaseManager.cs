using Microsoft.Data.SqlClient;
using System.Security.Cryptography;

namespace Infrastructure.Data;

public class DatabaseManager(string connectionString)
{
    private readonly string _connectionString = connectionString;

    private void InitializeDatabase()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            using (var command = new SqlCommand(
                "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Authentication') " +
                "BEGIN " +
                "CREATE TABLE Authentication (Id INT PRIMARY KEY IDENTITY(1,1), Username NVARCHAR(50) NOT NULL, PasswordHash NVARCHAR(100) NOT NULL); " +
                "END"
                ,
                connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    public bool ValidateUser(string username, string password)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SqlCommand(
                "SELECT PasswordHash FROM Authentications WHERE Username = @Username",
                connection))
            {
                command.Parameters.AddWithValue("@Username", username);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var hashedPasswordFromDatabase = reader["PasswordHash"].ToString();

                        if (hashedPasswordFromDatabase is null)
                            return false;
                        return VerifyPassword(password, hashedPasswordFromDatabase);
                    }
                }
            }
        }
        return false;
    }

    public bool RegisterUser(string username, string password)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            var hashedPassword = HashPassword(password);

            using (var command = new SqlCommand(
                "INSERT INTO Authentication (Username, PasswordHash) VALUES (@Username, @PasswordHash)",
                connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@PasswordHash", hashedPassword);

                return command.ExecuteNonQuery() > 0;
            }
        }
    }

    private bool VerifyPassword(string enteredPassword, string storedPasswordHash)
    {
        var enteredPasswordHash = HashPassword(enteredPassword);
        return string.Equals(enteredPasswordHash, storedPasswordHash, StringComparison.OrdinalIgnoreCase);
    }

    private string HashPassword(string password)
    {
        var passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
        var hashBytes = SHA256.HashData(passwordBytes);
        var hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

        return hashString;
    }
}
