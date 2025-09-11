using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesSystem.Entities;
using System.Data.SqlClient;

namespace SalesSystem.DAL
{
    internal class UserDAL : BaseDAL
    {
        public User Login(string email, string password)
        {
            User user = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT * 
                                 FROM [User] 
                                 WHERE Email=@Email 
                                 AND PasswordHash=@PasswordHash 
                                 AND Role IN ('Administrator','Seller')";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@PasswordHash", password);

                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    user = new User
                    {
                        UserID = (int)reader["UserID"],
                        FullName = reader["FullName"].ToString(),
                        IdentityNumber = (long)reader["IdentityNumber"],
                        Phone = reader["Phone"]?.ToString(),
                        Address = reader["Address"]?.ToString(),
                        Email = reader["Email"].ToString(),
                        RegistrationDate = (DateTime)reader["RegistrationDate"],
                        PasswordHash = reader["PasswordHash"].ToString(),
                        Role = reader["Role"].ToString()
                    };
                }
            }

            return user;
        }

        public List<User> GetAll()
        {
            var users = new List<User>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM [User]"; // Verifica que la tabla se llame así
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    users.Add(new User
                    {
                        UserID = (int)reader["UserID"],
                        FullName = reader["FullName"].ToString(),
                        IdentityNumber = (long)reader["IdentityNumber"],
                        Phone = reader["Phone"]?.ToString(),
                        Address = reader["Address"]?.ToString(),
                        Email = reader["Email"].ToString(),
                        RegistrationDate = (DateTime)reader["RegistrationDate"],
                        PasswordHash = reader["PasswordHash"].ToString(),
                        Role = reader["Role"].ToString()
                    });
                }
            }

            return users;
        }

        public void Delete(int userId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM [User] WHERE UserID = @UserID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserID", userId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public void Add(User user)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO [User] 
                        (FullName, IdentityNumber, Phone, Address, Email, PasswordHash, Role, RegistrationDate) 
                         VALUES (@FullName, @IdentityNumber, @Phone, @Address, @Email, @PasswordHash, @Role, @RegistrationDate)";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@FullName", user.FullName);
                cmd.Parameters.AddWithValue("@IdentityNumber", user.IdentityNumber);
                cmd.Parameters.AddWithValue("@Phone", user.Phone ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Address", user.Address ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                cmd.Parameters.AddWithValue("@Role", user.Role);
                cmd.Parameters.AddWithValue("@RegistrationDate", user.RegistrationDate);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public User GetById(int userId)
        {
            User user = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM [User] WHERE UserID = @UserID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserID", userId);

                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    user = new User
                    {
                        UserID = (int)reader["UserID"],
                        FullName = reader["FullName"].ToString(),
                        IdentityNumber = (long)reader["IdentityNumber"],
                        Phone = reader["Phone"]?.ToString(),
                        Address = reader["Address"]?.ToString(),
                        Email = reader["Email"].ToString(),
                        RegistrationDate = (DateTime)reader["RegistrationDate"],
                        PasswordHash = reader["PasswordHash"].ToString(),
                        Role = reader["Role"].ToString()
                    };
                }
            }
            return user;
        }

        public void Update(User user)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE [User] 
                         SET FullName=@FullName, IdentityNumber=@IdentityNumber, 
                             Phone=@Phone, Address=@Address, Email=@Email, 
                             PasswordHash=@PasswordHash, Role=@Role
                         WHERE UserID=@UserID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserID", user.UserID);
                cmd.Parameters.AddWithValue("@FullName", user.FullName);
                cmd.Parameters.AddWithValue("@IdentityNumber", user.IdentityNumber);
                cmd.Parameters.AddWithValue("@Phone", user.Phone ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Address", user.Address ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                cmd.Parameters.AddWithValue("@Role", user.Role);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }








    }
}

