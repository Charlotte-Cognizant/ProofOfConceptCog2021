using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task <ActionResult<IEnumerable<RegUser>>> GetUsers() 
        {
            return await _context.Users.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task <ActionResult<RegUser>> GetUser(int id) 
        {
            return await _context.Users.FindAsync(id);   
        }

        [HttpPost("addresssregister")]
        public async Task<ActionResult<AddressData>> pollAddress(string address, string city, string state, string zip) {
            var searchAddress = new AddressData{
                City=city,
                StreetAddress=address,
                State = state,
                Zip = zip
            };
            _context.adress.Add(searchAddress);
            await _context.SaveChangesAsync();
            return searchAddress;
        }
    
    }
}