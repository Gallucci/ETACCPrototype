using MVCandEntityFrameworkPractice.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace MVCandEntityFrameworkPractice.DataAccess
{
    public class ElectronicAccessContext : DbContext
    {
        // Specify the Connection String (default uses class name)
        public ElectronicAccessContext() : base("ElectronicAccessContext")
        {

        }

        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<AccessLevel> AccessLevels { get; set; }
        public DbSet<Person> Designees { get; set; }
        public DbSet<CardHolder> CardHolders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Prevents table names from being pluralized (i.e. "Catalog" instead of "Catalogs)
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Create a many-to-many relationship between AccessLevel and Card
            modelBuilder.Entity<Catalog>()
                .HasMany(catalog => catalog.AccessLevels).WithMany(acl => acl.Catalogs)
                .Map(table => table.MapLeftKey("CatalogId")
                    .MapRightKey("AccessLevelId")
                    .ToTable("CatalogAccessLevel"));

            // Create a many-to-many relationship between AccessLevel and Card
            modelBuilder.Entity<Card>()
                .HasMany(card => card.AccessLevels).WithMany(acl => acl.Cards)
                .Map(table => table.MapLeftKey("CardId")
                    .MapRightKey("AccessLevelId")
                    .ToTable("CardAccessLevel"));     
            
            //base.OnModelCreating(modelBuilder);
        }
    }
}