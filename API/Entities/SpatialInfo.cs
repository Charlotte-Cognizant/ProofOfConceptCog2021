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

        public System.Decimal Area { get; set; }

        public System.Double center_Lat { get; set; }

        public System.Double center_Long { get; set; }

        public byte[] imagebyte { get; set; }
        
    }
    //Json Lite loadable SQLite extension
    // One big Json!!
    // Delete Json from the project director to clear up space


}