using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCandEntityFrameworkPractice.Models.Domain.CardStatuses
{
    public class Issued : CardStatus
    {
        public Issued() 
        {
            Initialize();
        }

        public Issued(Card card) 
        {
            Initialize();
            this.Card = card;
        }

        public Issued(CardStatus status)
        {
            Initialize();
            this.Card = status.Card;
        }

        private void Initialize()
        {
            DisplayName = "Issued";
        }

        public override void Issue(CardHolder holder, DateTime startDate, DateTime endDate, int pin)
        {
            // Cannot issue a issued card
        }

        public override void Return()
        {
            // Return the card
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
            // Destroy the issued card
            this.Card.Status = new Destroyed(this);
            this.Card.StatusDate = DateTime.Now;
        }

        public override void Deactivate()
        {
            // Deactivate the issued card
            this.Card.Status = new Deactivated(this);
            this.Card.StatusDate = DateTime.Now;
        }

        public override void Lose()
        {
            // Lose the issued card
            this.Card.Status = new Lost(this);
            this.Card.StatusDate = DateTime.Now;
        }

        public override void Extend(DateTime extensionDate)
        {
            // Extend the issued card
            this.Card.EndDate = extensionDate;            
        }
    }
}