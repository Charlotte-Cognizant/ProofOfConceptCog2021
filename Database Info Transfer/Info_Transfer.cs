using System;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Database_Info_Transfer
{
    public class info_transfer
    {

        //methods of updating information




        //methods of inserting information
        //https://stackoverflow.com/questions/12142806/how-to-insert-records-in-database-using-c-sharp-language 
        //Would need to update the function's input variables to match the amount of stuff we want our databse to have
        public static void insertInfo()
        {
            string connetionString = null;
            string sql = null;

            // All the info required to reach your db. See connectionstrings.com
            connetionString = "Server=LAPTOP-FDBC786E\\SQLEXPRESS;Database=POC;Trusted_Connection=True";

            // Prepare a proper parameterized query 
            //The values will be replaced by the input variable strings
            sql = "INSERT INTO POC Footprint_Information (UID,Address,Perimeter,Area,Center_Point_X,Center_Point_Y,Date_Requested)" +
                "VALUES(1, 15504, 542101, 524653, 43523.25, 4856251, '2017-06-13')";

            // Create the connection (and be sure to dispose it at the end)
            using (SqlConnection cnn = new SqlConnection(connetionString))
            {
                try
                {
                    // Open the connection to the database. 
                    // This is the first critical step in the process.
                    // If we cannot reach the db then we have connectivity problems
                    cnn.Open();

                    // Prepare the command to be executed on the db
                    using (SqlCommand cmd = new SqlCommand(sql, cnn))
                    {

                        // Create and set the parameters values 
                        // The right hand side will be the variable names
                        cmd.Parameters.Add("@UID", SqlDbType.NVarChar).value = "1";
                        cmd.Parameters.Add("Address", SqlDbType.NVarChar).value = "15504";
                        cmd.Parameters.Add("Perimeter", SqlDbType.NVarChar).value = "542101";
                        cmd.Parameters.Add("Area", SqlDbType.NVarChar).value = "524653";
                        cmd.Parameters.Add("Center_Point_X", SqlDbType.NVarChar).value = "43523.25";
                        cmd.Parameters.Add("Center_Point_Y", SqlDbType.NVarChar).value = "4856251";
                        cmd.Parameters.Add("Date_Requested", SqlDbType.Date).value = "'2017-06-13'";

                        // Let's ask the db to execute the query
                        int rowsAdded = cmd.ExecuteNonQuery();
                        if (rowsAdded > 0)
                            MessageBox.Show("Row inserted!!");
                        else
                            // Well this should never really happen
                            MessageBox.Show("No row inserted");
                    }
                }


                catch (Exception ex)
                {
                    // We should log the error somewhere, 
                    // for this example let's just show a message
                    MessageBox.Show("ERROR:" + ex.Message);
                }
            }
        }


        // method of selecting and return information



    }
}