using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        //ID get request that returns a given users information placeholder function
        public async Task <ActionResult<RegUser>> GetUser(int id) 
        {
            return await _context.Users.FindAsync(id);   
        }

        [HttpPost("AdrPost")]
        //Takes user input as strings, (paramaters in http) and then returns an address data object. Adds addressData to the database. 
        //Todo update for DTO addressData
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


        private void runPythonScript(string cmd, string args){
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "C:/Documents/workcode/scripts";
            start.Arguments= string.Format("{0} {1}" , cmd, args);
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using (Process process = Process.Start(start)){
                using (StreamReader reader = process.StandardOutput){
                    string result = reader.ReadToEnd();
                    Console.Write (result);
                }
            }
            
        }



    }
}