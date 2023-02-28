﻿using Microsoft.EntityFrameworkCore;
using Sales.Shared.Entities;
using System.Reflection;

namespace Sales.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {

        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set;}
        public DbSet<Province> Provinces { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasIndex(c => c.Name).IsUnique();


            modelBuilder.Entity<Province>().HasIndex("CountryId", "Name").IsUnique();

            modelBuilder.Entity<City>().HasIndex("ProvinceId", "Name").IsUnique();

        }

    }
}
