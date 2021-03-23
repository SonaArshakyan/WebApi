using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApi.Models;

namespace WebApi.ProductManager
{
    public class DataAccessLayer
    {
        private readonly string connectionString;
        public DataAccessLayer()
        {
            connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        }

        public Product GetProduct(int id)
        {
            //connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

            Product product = new Product();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "sp_GetProduct";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            product.Id = Convert.ToInt32(reader["Id"]);
                            product.Name = reader["Name"].ToString();
                        }
                    }
                    else
                    {
                        Console.WriteLine("No rows found.");
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return product;
        }
    }
}