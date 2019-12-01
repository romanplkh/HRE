using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HorizonRE.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("ProjectConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        public DbSet<Employee> Employees { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ProvinceCustomer> ProvincesCustomers { get; set; }
        public DbSet<ProvinceEmployee> ProvincesEmployees { get; set; }
        public DbSet<ImageFile> Images { get; set; }
        public DbSet<Listing> Listings { get; set; }
        public DbSet<CityArea> CityAreas { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Feature> Features { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Province>()
               .HasRequired<Country>(c => c.Country)
               .WithMany(p => p.Provinces)
               .HasForeignKey<int>(b => b.CountryId)
               .WillCascadeOnDelete(false);


            modelBuilder.Entity<Listing>()
           .HasMany(c => c.Features)
           .WithMany(a => a.Listings)
           .Map(m =>
           {
               m.ToTable("ListingFeature");
               m.MapLeftKey("ListingId");
               m.MapRightKey("FeatureId");
           });



        }
    }
}