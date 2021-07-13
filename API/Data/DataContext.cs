using Microsoft.EntityFrameworkCore;
using API.Entities;

namespace API.Data

{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            //Set args in startup
            //Using existing entities, incorperating them with DbSet meants that EF (entity framework) puts them into the SQL database. You can include your own entities using this file
            //And a  created entity.
        }


        public DbSet<RegUser> Users {get;set;} 
        
        public DbSet<AddressData> adress{get; set;}

        public DbSet<SpatialInfo> spatial { get;set;}
    }
}