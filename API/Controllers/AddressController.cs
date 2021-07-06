using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController: ControllerBase
    {
        private readonly DataContext _context;
        public AddressController(DataContext context){
            _context = context;
        }
    
    
    }
    
}