using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Vera.Domain.Entity;

namespace Vera.Domain
{
    public class DatabaseContext : DbContext
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
    }
}