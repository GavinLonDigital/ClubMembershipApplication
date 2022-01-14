using ClubMembershipApplication.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ClubMembershipApplication.Data
{
    public class ClubMembershipDbContext : DbContext  //entityframeworkcore
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={AppDomain.CurrentDomain.BaseDirectory}ClubMembershipDb.db"); //in bin folder
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<User> Users { get; set; } //pass user type tp dbset (like a DAC!!)

    }
}
