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
                        cmd.Parameters.Add("1", SqlDbType.NVarChar);
                        cmd.Parameters.Add("15504", SqlDbType.NVarChar);
                        cmd.Parameters.Add("542101", SqlDbType.NVarChar);
                        cmd.Parameters.Add("524653", SqlDbType.NVarChar);
                        cmd.Parameters.Add("43523.25", SqlDbType.NVarChar);
                        cmd.Parameters.Add("4856251", SqlDbType.NVarChar);
                        cmd.Parameters.Add("'2017-06-13'", SqlDbType.Date);

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