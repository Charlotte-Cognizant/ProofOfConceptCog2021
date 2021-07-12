using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using static API.DTO.addressData;
using Microsoft.EntityFrameworkCore;
using API.DTO;

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
            //return await _context.Users.FindAsync(id);   
        }

        [HttpPost("AdrPost")]
        //Takes user input as strings, (paramaters in http) and then returns an address data object. Adds addressData to the database. 
        //Todo update for DTO addressData
        //Ima create a JSON to give here
        //Stream input information here https://stackoverflow.com/questions/40494913/how-to-read-request-body-in-an-asp-net-core-webapi-controller
        //Data json reading information :https://www.c-sharpcorner.com/article/working-with-json-string-in-C-Sharp/
        public async Task<ActionResult<AddressData>> pollAddress() {

            var request = HttpContext.Request;
            var stream = new StreamReader(request.Body);
            string results =await stream.ReadToEndAsync();

            Console.WriteLine(results);
            addressData outputObj = JsonSerializer.Deserialize<addressData>(results);
            
             var searchAddress = new AddressData{
                 City=outputObj.City,
                 StreetAddress=outputObj.Address,
                 State = outputObj.State,
                 Zip = outputObj.Zip
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
    //    [HttpPost("_____PLACEHOLDER")]



    }  
}