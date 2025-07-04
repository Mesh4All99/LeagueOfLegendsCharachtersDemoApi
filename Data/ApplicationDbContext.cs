using LeagueOfLegendsCharachters.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace LeagueOfLegendsCharachters.Data
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Charachter>()
                .HasOne(o => o.Status)
                .WithOne(o => o.Charachter)
                .HasForeignKey<Status>(o=>o.CharachterName);
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
        }
        public DbSet<Charachter> Charachters { get; set; }
        public DbSet<Status> Statuses { get; set; }
    }
}
