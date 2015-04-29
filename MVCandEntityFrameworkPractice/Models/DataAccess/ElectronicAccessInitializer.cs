using MVCandEntityFrameworkPractice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCandEntityFrameworkPractice.DAL
{
    public class ElectronicAccessInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ElectronicAccessContext>
    {
        protected override void Seed(ElectronicAccessContext context)
        {
            // Create a list of access levels
            var accessLevels = new List<AccessLevel>
            {
                new AccessLevel{Name="CORO Temp", DisplayName="Coronado Temporary", 
                    Cards = new List<Card>(), Catalogs = new List<Catalog>()},
                new AccessLevel{Name="GILA Temp", DisplayName="Gila Temporary", 
                    Cards = new List<Card>(), Catalogs = new List<Catalog>()},
                new AccessLevel{Name="GRGR Temp", DisplayName="Graham/Greenlee Temporary", 
                    Cards = new List<Card>(), Catalogs = new List<Catalog>()},
                new AccessLevel{Name="MARI Temp", DisplayName="Maricopa Temporary",
                    Cards = new List<Card>(), Catalogs = new List<Catalog>()}
            };

            // Add access levels to the AccessLevels entity set in the context and save the changes
            accessLevels.ForEach(acl => context.AccessLevels.Add(acl));
            context.SaveChanges();

            // Create a list of catalogs
            var catalogs = new List<Catalog>
            {
                new Catalog{DisplayName="Coronado", AccessLevels = new List<AccessLevel>(), Cards = new List<Card>()},
                new Catalog{DisplayName="Gila", AccessLevels = new List<AccessLevel>(), Cards = new List<Card>()},
                new Catalog{DisplayName="Graham/Greenlee", AccessLevels = new List<AccessLevel>(), Cards = new List<Card>()},
                new Catalog{DisplayName="Maricopa", AccessLevels = new List<AccessLevel>(), Cards = new List<Card>()}
            };

            // Add catalogs to the Catalogs entity set in the context and save the changes
            catalogs.ForEach(catalog => context.Catalogs.Add(catalog));
            context.SaveChanges();

            // Create a list of cards
            var cards = new List<Card>
            {
                new Card{Iso=1234123456785678, Pin=0, StartDate=null, EndDate=null, 
                    CatalogId=catalogs.Single(catalog => catalog.DisplayName == "Coronado").Id},
                new Card{Iso=1111111111111111, Pin=1234, StartDate=DateTime.Parse("2015-03-31 14:00"), EndDate=DateTime.Parse("2015-04-02 14:00"), 
                    CatalogId=catalogs.Single(catalog => catalog.DisplayName == "Coronado").Id},
                new Card{Iso=2222222222222222, Pin=0, StartDate=null, EndDate=null, 
                    CatalogId=catalogs.Single(catalog => catalog.DisplayName == "Gila").Id},
                new Card{Iso=3333333333333333, Pin=0, StartDate=null, EndDate=null, 
                    CatalogId=catalogs.Single(catalog => catalog.DisplayName == "Gila").Id},
                new Card{Iso=4444444444444444, Pin=0, StartDate=null, EndDate=null, 
                    CatalogId=catalogs.Single(catalog => catalog.DisplayName == "Graham/Greenlee").Id}
            };

            // Add cards to the Cards entity set in the context and save the changes
            cards.ForEach(card => context.Cards.Add(card));
            context.SaveChanges();

            AddAccessLevelsToCatalog(context, "Coronado", "CORO Temp");
            context.SaveChanges();
        }

        private void AddCardToCatalog(ElectronicAccessContext context, string catalogName, long cardIso)
        {
            var catalog = context.Catalogs.SingleOrDefault(c => c.DisplayName == catalogName);
            var card = context.Cards.SingleOrDefault(c => c.Iso == cardIso);

            catalog.Cards.Add(context.Cards.Single(c => c.Iso == cardIso));
        }

        private void AddAccessLevelsToCatalog(ElectronicAccessContext context, string catalogName, string planName)
        {
            var catalog = context.Catalogs.SingleOrDefault(c => c.DisplayName == catalogName);
            var acl = context.AccessLevels.SingleOrDefault(a => a.Name == planName);

            catalog.AccessLevels.Add(context.AccessLevels.SingleOrDefault(a => a.Name == planName));
        }
    }
}