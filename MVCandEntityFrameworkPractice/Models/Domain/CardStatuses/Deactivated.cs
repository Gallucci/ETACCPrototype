using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCandEntityFrameworkPractice.Models.Domain.CardStatuses
{
    class Deactivated : CardStatus
    {
        public Deactivated() 
        {
            Initialize();
        }

        public Deactivated(Card card) 
        {
            Initialize();
            this.Card = card;
        }

        public Deactivated(CardStatus status)
        {
            Initialize();
            this.Card = status.Card;
        }

        private void Initialize()
        {
            DisplayName = "Deactivated";
        }

        public override void Issue(CardHolder holder, DateTime startDate, DateTime endDate, int pin)
        {
            // Cannot issue a deactivated card
        }

        public override void Return()
        {
            // Return the destroyed card
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
            // Destroy the destroyed card
            this.Card.Status = new Destroyed(this);
            this.Card.StatusDate = DateTime.Now;
        }

        public override void Deactivate()
        {
            // Cannot deactivate a deactivate card
        }

        public override void Lose()
        {
            // Lose the deactivated card
            this.Card.Status = new Deactivated(this);
            this.Card.StatusDate = DateTime.Now;
        }

        public override void Extend(DateTime extensionDate)
        {
            // Cannot extend a deactivated card
        }
    }
}
