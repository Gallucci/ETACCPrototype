using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCandEntityFrameworkPractice.Models.Domain
{    
    public abstract class CatalogStatus
    {
        // Properties        
        public string DisplayName { get; set; }        
        public Catalog Catalog { get; set; }

        // Methods
        public abstract void Activate();
        public abstract void Deactivate();
    }
}