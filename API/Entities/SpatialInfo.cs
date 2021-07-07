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

        public System.Decimal Perimeter { get; set; }

        public System.Decimal Area { get; set; }

        public System.Double Center_Point_X { get; set; }

        public System.Double Center_Point_Y { get; set; }

        public System.DateTime Date_Accessed { get; set; }
        
        //Received the image hopefully through bytes
        public string imageByte {get;set; }

        //Creates the image itself and has the image of the specific byte to then store into database
        public ImageClass image = new ImageClass();
        image.imageToByte = imageByte;

        
    }


}