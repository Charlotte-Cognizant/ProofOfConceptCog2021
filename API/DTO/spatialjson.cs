using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class spatialjson
    {
       
            public string area {
                get;
                set;
            }
            public string center_lat {
                get;
                set;
            }
            public string center_long {
                get;
                set;
            }
            //Include all data from address object. TODO after refactoring and commenting 
        }
    }
