using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesSystem.Entities;
using System.Data.SqlClient;
using SalesSystem.DTOs;
using SalesSystem.DTOs.User;

namespace SalesSystem.DAL
{
    internal class UserDAL : BaseDAL
    {
        public User Login(UserLoginDTO loginDTO)
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
                cmd.Parameters.AddWithValue("@Email", loginDTO.Email);
                cmd.Parameters.AddWithValue("@PasswordHash", loginDTO.Password);

                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    user = new User
                    {
                        UserID = (int)reader["UserID"],
                        FullName = reader["FullName"].ToString(),
                        IdentityNumber = (long)reader["IdentityNumber"],
                        Phone = reader["Phone"].ToString(),
                        Address = reader["Address"].ToString(),
                        Email = reader["Email"].ToString(),
                        RegistrationDate = (DateTime)reader["RegistrationDate"],
                        PasswordHash = reader["PasswordHash"].ToString(),
                        Role = reader["Role"].ToString()
                    };
                }
            }

            return user;
        }
    }
}

