using System.Text.Json;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserRoles.configuration;
using UserRoles.Models;
using UserRoles.Models.Trek;

namespace UserRoles.Data
{
    public class AppDbContext : IdentityDbContext<Users>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CarousalImage> CarousalImages { get; set; }
        public DbSet<Deals> Deals { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<PageContent> PageContents { get; set; }
        public DbSet<NabBarContent> NavBarContents { get; set; }
        public DbSet<NavItem> NavItems { get; set; }

        public DbSet<TrekPackage> TrekPackages { get; set; }
        public DbSet<TrekPackageImage> TrekPackageImages { get; set; }
        public DbSet<TrekPackageCostInfo> TrekPackageCostInfos { get; set; }
        public DbSet<TrekPackageGroupPricing> TrekPackageGroupPricings { get; set; }
        public DbSet<TrekFAQ> TrekFAQs { get; set; }
        public DbSet<TrekItineraryDay> TrekItineraryDays { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CarousalImageConfiguration());
        }

    }
}
