using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoonBuck.Models;

namespace MoonBuck.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser, CustomRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<JobRole> JobRoles { get; set; }
        public DbSet<Slot> Slots { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<JobRole>().HasData(
                new JobRole { Id = 1, Name = "Chef", DisplayOrder= 1},
                new JobRole { Id = 2, Name = "Cashier", DisplayOrder = 2 },
                new JobRole { Id = 3, Name = "Waiter", DisplayOrder = 3 }
                );
            modelBuilder.Entity<Slot>().HasData(
                new Slot
                {
                    Id = 1,
                    Name = "MoonBuck Toa Payoh",
                    StartTime = DateTime.Now.AddDays(1),
                    EndTime = DateTime.Now.AddDays(1.4),
                    IsFilled = false,
                    PayRate = 9,
                    RoleId = 1,
                },
                new Slot
                {
                    Id = 2,
                    Name = "MoonBuck Toa Payoh",
                    StartTime = DateTime.Now.AddDays(2),
                    EndTime = DateTime.Now.AddDays(2.4),
                    IsFilled = false,
                    PayRate = 9,
                    RoleId = 2
                },
                new Slot
                {
                    Id = 3,
                    Name = "MoonBuck Tampines",
                    StartTime = DateTime.Now.AddDays(2),
                    EndTime = DateTime.Now.AddDays(2.4),
                    IsFilled = false,
                    PayRate = 9,
                    RoleId = 2
                },
                new Slot
                {
                    Id = 4,
                    Name = "MoonBuck Hougang",
                    StartTime = DateTime.Now.AddDays(3),
                    EndTime = DateTime.Now.AddDays(3.4),
                    IsFilled = false,
                    PayRate = 9,
                    RoleId = 2
                }
                );
            modelBuilder.Entity<Bid>().HasData(
                new Bid 
                { 
                    Id = 1,
                    StartTime = DateTime.Now.AddDays(1),
                    EndTime = DateTime.Now.AddDays(1.4),
                    Status = false,
                    SlotId = 1
                },
                new Bid
                {
                    Id = 2,
                    StartTime = DateTime.Now.AddDays(3),
                    EndTime = DateTime.Now.AddDays(3.4),
                    Status = false,
                    SlotId = 1
                },
                new Bid
                {
                    Id = 3,
                    StartTime = DateTime.Now.AddDays(2),
                    EndTime = DateTime.Now.AddDays(2.4),
                    Status = false,
                    SlotId = 1
                }
                );
        }
    }
}
