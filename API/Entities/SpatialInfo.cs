namespace API.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Drawing;

    public class SpatialInfo
    {
        //auto incremented id should match accordingly to the AddressData Entity
        public int ID { get; set; }

        public string address { get; set; }

        public string Area { get; set; }

        public string center_Lat { get; set; }

        public string center_Long { get; set; }

        public DateTime dateaccessed { get; set; }

        public byte[] imagebyte { get; set; }
        
        public string imagePath{get; set;}
        
    }
    //Json Lite loadable SQLite extension
    // One big Json!!
    // Delete Json from the project director to clear up space


}