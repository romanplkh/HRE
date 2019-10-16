using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HorizonRE.Models
{
    public class HorizonContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ProvinceCustomer> ProvincesCustomers { get; set; }
        public  DbSet<ProvinceEmployee> ProvincesEmployees { get; set; }
        public  DbSet<ImageFile> Images { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Province>()
               .HasRequired<Country>(c => c.Country)
               .WithMany(p => p.Provinces)
               .HasForeignKey<int>(b => b.CountryId)
               .WillCascadeOnDelete(false);

        }

    }


}