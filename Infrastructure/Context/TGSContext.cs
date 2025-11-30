using Domain.Model;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public class TGSContext : DbContext
    {
        public TGSContext(DbContextOptions<TGSContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new DestinationConfiguration());
        }


        public DbSet<Destination> Destinations { get; set; }
    }
}
