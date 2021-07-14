namespace API.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Drawing;

    public class Images
    {
        //auto incremented id should match accordingly to the AddressData Entity
        public int ID { get; set; }

        public byte[] image { get; set; }
    }
}