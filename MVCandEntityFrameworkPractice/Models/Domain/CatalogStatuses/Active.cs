using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCandEntityFrameworkPractice.Models.Domain.CatalogStatuses
{
    public class Active : CatalogStatus
    {
        public Active() 
        {
            Initialize();
        }

        public Active(Catalog catalog)
        {
            Initialize();
            this.Catalog = catalog;
        }

        public Active(CatalogStatus status)
        {
            Initialize();
            this.Catalog = status.Catalog;
        }

        private void Initialize()
        {
            DisplayName = "Active";
        }

        public override void Activate()
        {
            //You cannot activate an active catalog
        }

        public override void Deactivate()
        {
            this.Catalog.Status = new Inactive(this);
            this.Catalog.StatusDate = DateTime.Now;            
        }
    }
}