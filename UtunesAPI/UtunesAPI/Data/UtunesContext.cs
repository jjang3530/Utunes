using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UtunesAPI.Models;

namespace UtunesAPI.Data
{
    public class UtunesContext : DbContext
    {
        public UtunesContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Review> Review { get; set; }
        public DbSet<Song> Song { get; set; }
    }
}
