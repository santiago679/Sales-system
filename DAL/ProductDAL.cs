using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using SalesSystem.Entities;

namespace SalesSystem.DAL
{
    public class ProductDAL
    {
        private readonly string connectionString;

        public ProductDAL()
        {
            connectionString = ConfigurationManager
                .ConnectionStrings["SalesSystemDB"]
                .ConnectionString;
        }

        // Creación de nuevo producto
        public void AddProduct(Product product)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Product (ProductName, Description, Price, IsActive)
                                 VALUES (@ProductName, @Description, @Price, @IsActive)";

                //Es buena practica poner la consulta como variable
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                cmd.Parameters.AddWithValue("@Description", (object)product.Description ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@IsActive", product.IsActive);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Obtener todos los productos
        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT ProductID, ProductName, Description, Price, IsActive FROM Product";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    products.Add(new Product
                    {
                        ProductID = (int)reader["ProductID"],
                        ProductName = reader["ProductName"].ToString(),
                        Description = reader["Description"]?.ToString(),
                        Price = (decimal)reader["Price"],
                        IsActive = (bool)reader["IsActive"]
                    });
                }
            }

            return products;
        }

        // Obetner producto por su ID
        public Product GetProductById(int id)
        {
            Product product = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT ProductID, ProductName, Description, Price, IsActive FROM Product WHERE ProductID = @ProductID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ProductID", id);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    product = new Product
                    {
                        ProductID = (int)reader["ProductID"],
                        ProductName = reader["ProductName"].ToString(),
                        Description = reader["Description"]?.ToString(),
                        Price = (decimal)reader["Price"],
                        IsActive = (bool)reader["IsActive"]
                    };
                }
            }

            return product;
        }

        // Actualizar producto
        public void UpdateProduct(Product product)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Product 
                                 SET ProductName=@ProductName, Description=@Description, Price=@Price, IsActive=@IsActive
                                 WHERE ProductID=@ProductID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    //El ProductID lo pasamos como parámetro para la consulta sin modificarlo
                    cmd.Parameters.AddWithValue("@ProductID", product.ProductID);
                    cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                    cmd.Parameters.AddWithValue("@Description", (object)product.Description ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Price", product.Price);
                    cmd.Parameters.AddWithValue("@IsActive", product.IsActive);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                } ;
            }
        }

        // Eliminar producto
        public void DeleteProduct(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Product WHERE ProductID=@ProductID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ProductID", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
