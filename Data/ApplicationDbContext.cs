using LeagueOfLegendsCharachters.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace LeagueOfLegendsCharachters.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Charachter>()
                .HasOne(o => o.Status)
                .WithOne(o => o.Charachter)
                .HasForeignKey<Status>(o => o.CharachterName);
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
        }
        public DbSet<Charachter> Charachters { get; set; }
        public DbSet<Status> Statuses { get; set; }
    }
}
