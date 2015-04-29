using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCandEntityFrameworkPractice.Models.Domain
{
    public class CardHolder : Student
    {
        public CardHolder()
        {
            this.Cards = new List<Card>();
        }

        //Properties
        public int? DesigneeId { get; set; }

        // Navigational Properties
        public Person Designee { get; set; }
        public IList<Card> Cards { get; set; }
    }
}