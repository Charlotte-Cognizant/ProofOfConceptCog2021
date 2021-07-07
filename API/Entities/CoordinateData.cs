namespace API.Entities
{
    public class coordinateData
    {
        //auto incremented id
        //Entity object that includes the information contained in any row in our Address table. 
        public int ID{get; set;}
    
        public double xCord{get; set;}

        public double yCord{get;set;}

        public string address{get; set;}

        public string city {get; set;}


    }
}