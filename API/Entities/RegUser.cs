namespace API.Entities
{
    public class RegUser
    {
        public int ID { get; set; }    

        public string RegUserName {get; set;}

        public  string userEmail { get; set; }
    
    }

//user needs to be able to access database but only to make a pull request. On the database end we check if we have, if not access via api and then return address from db. 




}