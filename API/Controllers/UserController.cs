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
using System.Drawing;
using Newtonsoft.Json;
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

        public Image BytetoImage(byte[] imageArray)
        {
            using (MemoryStream ms = new MemoryStream(imageArray))
            {
                return Image.FromStream(ms);
            }
        }

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

        public string notAvailError(){
            return ("Sorry, the address number you entered is not available at this time.");
        }

        public string notFoundError(){
            return ("Sorry, we cannot find the address you entered. Check for errors and try again.");

        }

        public string notDetectedError(){
            return ("Sorry, we did not detect any buildings at the address entered.");
        }



        private void runPythonScript(AddressData address){

            string address_str = String.Format("{0}, {1}, {2} {3}", address.StreetAddress, address.City, address.State, address.Zip);

            // Set working directory and create process
            var workingDirectory = "C:/Documents/workcode/scripts";
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    WorkingDirectory = workingDirectory
                }
            };
            process.Start();
            // Pass multiple commands to cmd.exe
            using (var sw = process.StandardInput)
            {
                if (sw.BaseStream.CanWrite)
                {
                    // Vital to activate Anaconda
                    sw.WriteLine("C:\\Users\\hartc\\anaconda3\\Scripts\\activate.bat");
                    // Activate ox environment
                    sw.WriteLine("conda activate ox");
                    // set environment variables and init mapbox api
                    sw.WriteLine("set MAPBOX_ACCESS_TOKEN=pk.eyJ1IjoiaGFydGMxNyIsImEiOiJja3IyNWxmMGQyODZyMnB0OXJlOHd4ZGJrIn0.2abXKt7EfUNNHWzvj6buRg");
                    sw.WriteLine("mapbox ...");
                    // run your script. You can also pass in arguments
                    sw.WriteLine(string.Format("python script.py '{1}' geojson" , address_str));
                }
            }
            // read multiple output lines
            while (!process.StandardOutput.EndOfStream)
            {
                var line = process.StandardOutput.ReadLine();
                if (line == "Sorry, address number not available at this time"){
                    notAvailError();
                }
                else if (line == "Address cannot be found."){
                    notFoundError();
                }
                else if (line == "No building detected at given address."){
                    notDetectedError();
                }
            }


/*          //old code
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
            */
        }



    }
}
