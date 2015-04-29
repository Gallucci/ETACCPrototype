using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCandEntityFrameworkPractice.Models.Domain
{
    public class AccessLevel
    {
        public AccessLevel()
        {
            this.Catalogs = new List<Catalog>();
            this.Cards = new List<Card>();
        }

        // Properties
        public int Id { get; set; }
        
        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }

        [Required]
        [Display(Name = "DSX Name")]
        public string InternalName { get; set; }


        // Navigation Properties
        public IList<Catalog> Catalogs { get; set; }
        public IList<Card> Cards { get; set; }
    }
}