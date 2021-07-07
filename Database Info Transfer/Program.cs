using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Sql;



namespace Database_Info_Transfer
{
    public class Program
    {
        //public System.Data.SqlClient.SqlParameterCollection Parameters { get; }

        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            Info_Transfer testing = new Info_Transfer();
            testing.insertInfo();
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });




        // This is function for SQL update
        /*private static void UpdateDemographics(Int32 customerID,
    string demoXml, string connectionString)
        {
            // Update the demographics for a store, which is stored
            // in an xml column.
            string commandText = "INSERT Sales.Store SET Demographics = @demographics "
                + "WHERE CustomerID = @ID;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                command.Parameters.Add("@ID", SqlDbType.Int);
                command.Parameters["@ID"].Value = customerID;

                // Use AddWithValue to assign Demographics.
                // SQL Server will implicitly convert strings into XML.
                command.Parameters.AddWithValue("@demographics", demoXml);

                try
                {
                    connection.Open();
                    Int32 rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine("RowsAffected: {0}", rowsAffected);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }*/
    }
}
