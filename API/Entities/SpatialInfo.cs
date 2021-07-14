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

        public string Area { get; set; }

        public string center_Lat { get; set; }

        public string center_Long { get; set; }
        
        //Received the image hopefully through bytes
        public byte[] imageByte {get;set; }
        
    }
    //Json Lite loadable SQLite extension
    // One big Json!!
    // Delete Json from the project director to clear up space


}