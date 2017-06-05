using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Vera.Domain.Entity;
using Vera.Domain.Entity.Identity;

namespace Vera.Domain
{
    public class DatabaseContext : IdentityDbContext<User>
    {
        public DatabaseContext() : base("VeraDB") { }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Format> Formats { get; set; }
        public DbSet<GluePrice> GluePrices { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<FormingType> FormingTypes { get; set; }
        public DbSet<Sewing> Sewings { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<MaterialType> MaterialTypes { get; set; }
        public DbSet<Colorfulness> Colorfulnesses { get; set; }
        public DbSet<ColorfulnessPricePerFormat> ColorfulnessPricePerFormats { get; set; }
    }
}