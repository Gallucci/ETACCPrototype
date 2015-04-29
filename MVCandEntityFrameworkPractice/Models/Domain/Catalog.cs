using MVCandEntityFrameworkPractice.Models.Domain.CatalogStatuses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCandEntityFrameworkPractice.Models.Domain
{
    public class Catalog
    {
        public Catalog()
        {
            this.AccessLevels = new List<AccessLevel>();
            this.Cards = new List<Card>();
            this.Status = new Active(this);

            this.CreatedDate = System.DateTime.Now;
            this.ModifiedDate = System.DateTime.Now;            
            this.StatusDate = System.DateTime.Now;
        }

        // Properties
        public int Id { get; set; }

        [Required]
        [Display(Name="Name")]
        public string DisplayName { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm tt}")]
        [Display(Name = "Created")]
        public DateTime CreatedDate { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm tt}")]
        [Display(Name = "Last Modified")]
        public DateTime ModifiedDate { get; set; }

        [Display(Name = "Status")]
        public string StatusType
        { 
            get
            {
                if (Status == null)
                    Status = new Active();

                return Status.DisplayName;
            }
            set
            {
                var status = CatalogStatusFactory.GetStatus(value);
                status.Catalog = this;
                Status = status;
            }
        }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm tt}")]
        [Display(Name = "Status Changed")]
        public DateTime StatusDate { get; set; }

        // Unmapped Properties

        [NotMapped]
        public CatalogStatus Status { get; set; }

        // Navigation Properties
        public virtual IList<Card> Cards { get; set; }

        [Display(Name="Access Levels")]
        public virtual IList<AccessLevel> AccessLevels {get; set;}
    }
}