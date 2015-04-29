using MVCandEntityFrameworkPractice.Models.Domain.CardStatuses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCandEntityFrameworkPractice.Models.Domain
{
    public class Card
    {
        public Card()
        {
            this.AccessLevels = new List<AccessLevel>();
            this.Status = new InStock(this);

            this.CreatedDate = System.DateTime.Now;
            this.ModifiedDate = System.DateTime.Now;
            this.StatusDate = System.DateTime.Now;
        }

        // Properties
        public int Id { get; set; }
        public int? CatalogId { get; set; }  // Foreign Key {navigation property name}{primary key property name}
        public int? HolderId { get; set; }  // Foreign Key {navigation property name}{primary key property name}

        [Required]
        [Display(Name = "ISO Number")]
        public long Iso { get; set; }

        [Display(Name="PIN")]
        public int? Pin { get; set; }
        
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString= "{0:yyyy-MM-dd hh:mm tt}")]
        [Display(Name="Access Starts")]
        public DateTime? StartDate {get; set;}
        
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm tt}")]
        [Display(Name="Access Ends")]
        public DateTime? EndDate { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm tt}")]
        [Display(Name = "Created")]
        public DateTime? CreatedDate { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm tt}")]
        [Display(Name = "Last Modified")]
        public DateTime? ModifiedDate { get; set; }

        [Display(Name = "Status")]
        public string StatusType
        {
            get
            {
                if (Status == null)
                    Status = new InStock();

                return Status.DisplayName;
            }
            set
            {
                var status = CardStatusFactory.GetStatus(value);
                status.Card = this;
                Status = status;
            }
        }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm tt}")]
        [Display(Name = "Status Changed")]
        public DateTime? StatusDate { get; set; }

        // Unmapped Properties

        [NotMapped]
        public CardStatus Status { get; set; }

        // Navigation Properties
        public virtual CardHolder Holder { get; set; }
        public virtual Catalog Catalog { get; set; }
        public virtual IList<AccessLevel> AccessLevels { get; set; }
    }
}