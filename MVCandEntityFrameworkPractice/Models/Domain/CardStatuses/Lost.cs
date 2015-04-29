using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCandEntityFrameworkPractice.Models.Domain.CardStatuses
{
    public class Lost : CardStatus
    {
        public Lost() 
        {
            Initialize();
        }

        public Lost(Card card) 
        {
            Initialize();
            this.Card = card;
        }

        public Lost(CardStatus status)
        {
            Initialize();
            this.Card = status.Card;
        }

        private void Initialize()
        {
            DisplayName = "Lost";
        }

        public override void Issue(CardHolder holder, DateTime startDate, DateTime endDate, int pin)
        {
            // Cannot issue a lost card
        }

        public override void Return()
        {
            // Return the lost card
            this.Card.Holder = null;
            this.Card.StartDate = null;
            this.Card.EndDate = null;
            this.Card.Pin = null;
            this.Card.AccessLevels = null;

            this.Card.Status = new InStock(this);
            this.Card.StatusDate = DateTime.Now; 
        }

        public override void Destroy()
        {
            // Destroy the lost card
            this.Card.Status = new Destroyed(this);
            this.Card.StatusDate = DateTime.Now;
        }

        public override void Deactivate()
        {
           // Deactivate the lost card
            this.Card.Status = new Deactivated(this);
            this.Card.StatusDate = DateTime.Now;
        }

        public override void Lose()
        {
            // Cannot lose a lost card
        }

        public override void Extend(DateTime extensionDate)
        {
            // Cannot extend a lost card
        }
    }
}