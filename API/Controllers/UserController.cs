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

        public async Task<ActionResult<SpatialInfo>> pollSpatial(Decimal perimeter, Decimal area, double center_Point_X, double center_Point_Y, byte[] image)
        {
            
            var spatialinfo = new SpatialInfo {
                Perimeter = perimeter,
                Area = area,
                Center_Point_X = center_Point_X,
                Center_Point_Y = center_Point_Y,
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

        public Image BytetoImage(byte [] imageArray)
        {
            using (MemoryStream ms = new MemoryStream(imageArray))
            {
                return Image.FromStream(ms);
            }
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