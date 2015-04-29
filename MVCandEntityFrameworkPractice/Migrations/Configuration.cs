namespace MVCandEntityFrameworkPractice.Migrations
{
    using MVCandEntityFrameworkPractice.DataAccess;
    using MVCandEntityFrameworkPractice.Models.Domain;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MVCandEntityFrameworkPractice.DataAccess.ElectronicAccessContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MVCandEntityFrameworkPractice.DataAccess.ElectronicAccessContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            // Create a list of access levels
            var accessLevels = new List<AccessLevel>
            {
                new AccessLevel{
                    InternalName="CORO Temp Card", 
                    DisplayName="Coronado Temporary Access", 
                    Cards = new List<Card>(), 
                    Catalogs = new List<Catalog>()},
                new AccessLevel{
                    InternalName="CORO Ext", 
                    DisplayName="Coronado Extended Access", 
                    Cards = new List<Card>(), 
                    Catalogs = new List<Catalog>()},
                new AccessLevel{
                    InternalName="GILA Temp Card", 
                    DisplayName="Gila Temporary Access", 
                    Cards = new List<Card>(), 
                    Catalogs = new List<Catalog>()},
                new AccessLevel{
                    InternalName="GRGR Temp Card", 
                    DisplayName="Graham/Greenlee Temporary Access", 
                    Cards = new List<Card>(), 
                    Catalogs = new List<Catalog>()},
                new AccessLevel{
                    InternalName="MARI Temp Card", 
                    DisplayName="Maricopa Temporary Access",
                    Cards = new List<Card>(), 
                    Catalogs = new List<Catalog>()}
            };

            // Add access levels to the AccessLevels entity set in the context and save the changes
            accessLevels.ForEach(acl => context.AccessLevels.AddOrUpdate(a => a.InternalName, acl));            
            context.SaveChanges();

            // Create a list of catalogs
            var catalogs = new List<Catalog>
            {
                new Catalog{
                    DisplayName="Coronado", 
                    AccessLevels = new List<AccessLevel>()},
                new Catalog{
                    DisplayName="Gila", 
                    AccessLevels = new List<AccessLevel>()},
                new Catalog{
                    DisplayName="Graham/Greenlee", 
                    AccessLevels = new List<AccessLevel>()},
                new Catalog{
                    DisplayName="Maricopa", 
                    AccessLevels = new List<AccessLevel>()}
            };

            // Create a list of designees
            var designees = new List<Person>
            {
                new Person{
                    LastName = "Baxter",
                    FirstName = "Bill"
                }
            };
            designees.ForEach(designee => context.Designees.AddOrUpdate(d => d.LastName, designee));
            context.SaveChanges();

            // Create a list of holders
            var holders = new List<CardHolder>
            {
                new CardHolder{
                    LastName = "Nierenhausen",
                    FirstName = "Chad",
                    StudentId = 12345678,
                    NetId = "cnieren",
                    DesigneeId = designees.Single(designee => designee.LastName == "Baxter" && designee.FirstName == "Bill").Id}                
            };
            holders.ForEach(holder => context.CardHolders.AddOrUpdate(h => h.NetId, holder));
            context.SaveChanges();            

            // Add catalogs to the Catalogs entity set in the context and save the changes    
            catalogs.ForEach(catalog => context.Catalogs.AddOrUpdate(c => c.DisplayName, catalog));
            context.SaveChanges();

            // Create a list of cards
            var cards = new List<Card>
            {
                new Card{
                    Iso=1234123456785678, 
                    Pin=null, 
                    StartDate=null, 
                    EndDate=null, 
                    CatalogId=catalogs.Single(catalog => catalog.DisplayName == "Coronado").Id},
                new Card{
                    Iso=1111111111111111, 
                    Pin=null, 
                    StartDate=null, 
                    EndDate=DateTime.Parse("2015-04-02 14:00"), 
                    CatalogId=catalogs.Single(catalog => catalog.DisplayName == "Coronado").Id},
                new Card{
                    Iso=2222222222222222, 
                    Pin=null, 
                    StartDate=null, 
                    EndDate=null, 
                    CatalogId=catalogs.Single(catalog => catalog.DisplayName == "Gila").Id},
                new Card{
                    Iso=3333333333333333, 
                    Pin=null, 
                    StartDate=null, 
                    EndDate=null, 
                    CatalogId=catalogs.Single(catalog => catalog.DisplayName == "Gila").Id},
                new Card{
                    Iso=4444444444444444, 
                    Pin=null,
                    StartDate=null, 
                    EndDate=null, 
                    CatalogId=catalogs.Single(catalog => catalog.DisplayName == "Graham/Greenlee").Id}
            };

            // Add cards to the Cards entity set in the context and save the changes
            cards.ForEach(card => context.Cards.AddOrUpdate(c => c.Iso, card));

            // Add access levels
            AddAccessLevelsToCatalog(context, "Coronado", "CORO Temp Card");
            AddAccessLevelsToCatalog(context, "Coronado", "CORO Ext");
            AddAccessLevelsToCatalog(context, "Gila", "GILA Temp Card");
            AddAccessLevelsToCatalog(context, "Graham/Greenlee", "GRGR Temp Card");
            AddAccessLevelsToCatalog(context, "Maricopa", "MARI Temp Card");
            context.SaveChanges();

            // Do some actions
            var cardHolder = holders.Single(holder => holder.NetId == "cnieren");

            context.Catalogs.SingleOrDefault(c => c.DisplayName == "Maricopa").Status.Deactivate();
            context.Cards.SingleOrDefault(c => c.Iso == 1111111111111111).Status.Issue(cardHolder, new DateTime(2015, 3, 31), new DateTime(2015, 3, 31), 1234);
            context.SaveChanges();
        }

        private void AddAccessLevelsToCatalog(ElectronicAccessContext context, string catalogName, string planName)
        {
            var catalog = context.Catalogs.SingleOrDefault(c => c.DisplayName == catalogName);
            var acl = context.AccessLevels.SingleOrDefault(a => a.InternalName == planName);

            if(catalog != null && acl != null && !catalog.AccessLevels.Contains(acl))
                catalog.AccessLevels.Add(acl);
        }        
    }
}
