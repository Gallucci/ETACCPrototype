using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCandEntityFrameworkPractice.Models.Domain.CatalogStatuses
{
    public class Inactive : CatalogStatus
    {
        public Inactive() 
        {
            Initialize();
        }

        public Inactive(Catalog catalog)
        {
            Initialize();
            this.Catalog = catalog;
        }

        public Inactive(CatalogStatus status)
        {
            Initialize();
            this.Catalog = status.Catalog;
        }

        private void Initialize()
        {
            DisplayName = "Inactive";
        }

        public override void Activate()
        {
            this.Catalog.Status = new Active(this);
            this.Catalog.StatusDate = DateTime.Now;
        }

        public override void Deactivate()
        {
            // An inactive catalog cannot be deactivated
        }
    }
}