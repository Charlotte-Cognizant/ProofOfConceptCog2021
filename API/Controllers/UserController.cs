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
        public async Task<ActionResult<IEnumerable<RegUser>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }


        [HttpGet("{id}")]
        //ID get request that returns a given users information placeholder function
        public async Task<ActionResult<RegUser>> GetUser(int id)
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
        public async Task<ActionResult<AddressData>> pollAddress()
        {
            var request = HttpContext.Request;
            var stream = new StreamReader(request.Body);
            string results = await stream.ReadToEndAsync();

            Console.WriteLine(results);
            addressData outputObj = JsonSerializer.Deserialize<addressData>(results);

            var searchAddress = new AddressData {
                City = outputObj.City,
                StreetAddress = outputObj.Address,
                State = outputObj.State,
                Zip = outputObj.Zip
            };
            runPythonScript(searchAddress);
            
            _context.adress.Add(searchAddress);

            await _context.SaveChangesAsync();
            return searchAddress;
        }

        public string notAvailError()
        {
            return ("Sorry, the address number you entered is not available at this time.");
        }

        public string notFoundError()
        {
            return ("Sorry, we cannot find the address you entered. Check for errors and try again.");

        }

        public string notDetectedError()
        {
            return ("Sorry, we did not detect any buildings at the address entered.");
        }



        private void runPythonScript(AddressData address)
        {
            string address_str = String.Format("{0},{1},{2}", address.StreetAddress, address.City, address.State);
            address_str=address_str+String.Format(" {0}", address.Zip);
            //Console.Write(address_str);
            // Set working directory and create process
            var workingDirectory = "C:/Users/jroux/Documents/workcode/scripts";
            var process = new Process {
                StartInfo = new ProcessStartInfo {
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    WorkingDirectory = workingDirectory
                }
            };
            process.Start();
            Console.WriteLine("Passing process.write");
            // Pass multiple commands to cmd.exe
            using (var sw = process.StandardInput)
            {
                if (sw.BaseStream.CanWrite)
                {
                    // Vital to activate Anaconda
                    sw.WriteLine("C:\\Users\\jroux\\anaconda3\\Scripts\\activate.bat");
                    // Activate ox environment
                    sw.WriteLine("conda activate ox");
                    // set environment variables and init mapbox api
                    sw.WriteLine("set MAPBOX_ACCESS_TOKEN=pk.eyJ1IjoiaGFydGMxNyIsImEiOiJja3IyNWxmMGQyODZyMnB0OXJlOHd4ZGJrIn0.2abXKt7EfUNNHWzvj6buRg");
                    sw.WriteLine("mapbox ...");
                    // run your script. You can also pass in arguments
                    Console.Write(string.Format("python script.py \"{0}\" geojson", address_str));
                    sw.WriteLine(string.Format("python script.py \"{0}\" geojson", address_str));
                }
            }
            // read multiple output lines
            while (!process.StandardOutput.EndOfStream)
            {
                var line = process.StandardOutput.ReadLine();
                if (line == "Sorry, address number not available at this time")
                {
                    Console.WriteLine(notAvailError());
                }
                else if (line == "Address cannot be found.")
                {
                    Console.WriteLine(notFoundError());
                }
                else if (line == "No building detected at given address.")
                {
                    Console.WriteLine(notDetectedError());
                }
            }
        }


        private void runPythonScript(string cmd, string args)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "C:/Documents/workcode/scripts";
            start.Arguments = string.Format("{0} {1}", cmd, args);
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    spatialjson spatialholder = JsonSerializer.Deserialize<spatialjson>(result);

                    string imagePath = "./imagery/";
                    FileStream filestream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
                    byte[] imageByteArray = new byte[filestream.Length];
                    filestream.Read(imageByteArray, 0, imageByteArray.Length);

                    var spatialinfo = new SpatialInfo {
                        ID = spatialholder.uniqueID,
                        Area = spatialholder.area,
                        center_Lat = spatialholder.center_lat,
                        center_Long = spatialholder.center_long,
                        dateaccessed = spatialholder.date,
                        imagebyte = imageByteArray,
                    };

                    _context.spatial.Add(spatialinfo);
                    _context.SaveChangesAsync();
                    Console.Write(result);
                }
            }

        }

        /*public async Task<ActionResult<SpatialInfo>> pollSpatial(string area, string Center_Lat, string Center_Long, byte[] image)
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

        public byte[] ImagetoByte ()
        {
            //missing directory ./imagery/
            string imagePath = "./imagery/";
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

   */

        /*//[HttpGet]
        public async Task<object> jsonGet()
        {
            var request = HttpContext.Request;
            var stream = new StreamReader(request.Body);
            string results = await stream.ReadToEndAsync();

            Console.WriteLine(results);
            spatialjson spatialholder = JsonSerializer.Deserialize<spatialjson>(results);
        }
        //    [HttpPost("_____PLACEHOLDER")]

        */

    }
}