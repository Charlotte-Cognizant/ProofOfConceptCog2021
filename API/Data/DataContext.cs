using Microsoft.EntityFrameworkCore;
using API.Entities;

namespace API.Data

{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            //Set args in startup
        }


        public DbSet<RegUser> Users {get;set;} 
    }
}