namespace API.DTO
{
    public class addressData
    {
        public string Address{
        get;
        set;
        }
        public string City{
            get;
            set;
        }
        public string State{
            get;
            set;
        }
        public string Zip{
            get;
            set;
        }
        //Include all data from address object. TODO after refactoring and commenting 
    }
}