using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
//Controller that incorperates the database context and nothing else. Can be used for additional HTTPS requests etc and other api functions. No defined function atm
{
    [ApiController]
    [Route("api/[controller]")]
    public class addressPageController: ControllerBase
    {
        private readonly DataContext _context;
        public addressPageController(DataContext context){
            _context = context;
        }

    [HttpGet()]
    public async Task<ActionResult<IEnumerable<AddressData>>> getAddressList(){
        return await _context.Address.ToListAsync();
    }
    [HttpGet("{id}")]
    public async Task <ActionResult<SpatialInfo>> getImageAddress(int id)
    {return await _context.spatial.FindAsync(id);}
    
    
    
    
    
    
    
    
    }
    
}