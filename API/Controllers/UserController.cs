using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using API.DTO;
using System.Net.Http;

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

        public async Task<ActionResult<SpatialInfo>> pollSpatial(Decimal area, double Center_Lat, double Center_Long, byte[] image)
        {
            var spatialinfo = new SpatialInfo {
                Area = area,
                center_Lat = Center_Lat,
                center_Long = Center_Long,
                imageByte = image,

            };
            _context.spatial.Add(spatialinfo);
            await _context.SaveChangesAsync();

            return spatialinfo;
        }

        public byte[] ImagetoByte (string imagePath)
        {
            FileStream filestream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
            byte[] imageByteArray = new byte[filestream.Length];

            filestream.Read(imageByteArray, 0, imageByteArray.Length);


            return imageByteArray;
        }

        // public Image BytetoImage(byte[] imageArray)
        // {
        //     using (MemoryStream ms = new MemoryStream(imageArray))
        //     {
        //         return Image.FromStream(ms);
        //     }
        // }

        [HttpGet]
        public static async Task<object> jsonGet()
        {
            try
            {
                using(HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync("http://localhost.com/5001/0");

                }
            }
            catch
            {

            }
            return null;
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