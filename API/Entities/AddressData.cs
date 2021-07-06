namespace API.Entities
{
    public class AddressData
    {
        //auto incremented id
        //Entity object that includes the information contained in any row in our Address table. 
        public int ID{get; set;}
    
        public string StreetAddress{get; set;}

        public string Zip{get;set;}

        public string State{get; set;}

        public string City {get; set;}


    }
}