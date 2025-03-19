using LicenceGenAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LicenceGenAPI.DbConnection
{
    public class PostgreDbContext : DbContext
    {
        public PostgreDbContext()
        {
            
        }

        public PostgreDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<LicenceModel> Licence { get; set; }
    }
}
