using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VehicleAPI.Models;

namespace VehicleAPI.Data
{
    public class VehicleDbContext : IdentityDbContext<IdentityUser>
    {
        public VehicleDbContext(DbContextOptions<VehicleDbContext> options) : base(options)
        {

        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Category> Categories { get; set; }
    }

}
