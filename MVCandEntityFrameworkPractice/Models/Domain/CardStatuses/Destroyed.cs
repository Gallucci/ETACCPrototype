using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCandEntityFrameworkPractice.Models.Domain.CardStatuses
{
    class Destroyed : CardStatus
    {
        public Destroyed() 
        {
            Initialize();
        }

        public Destroyed(Card card) 
        {
            Initialize();
            this.Card = card;
        }

        public Destroyed(CardStatus status)
        {
            Initialize();
            this.Card = status.Card;
        }

        private void Initialize()
        {
            DisplayName = "Destroyed";
        }

        public override void Issue(CardHolder holder, DateTime startDate, DateTime endDate, int pin)
        {
            // Cannot issue a destroyed card
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
            // Cannot destroy a destroyed card
        }

        public override void Deactivate()
        {
            // Deactivate the destroyed card
            this.Card.Status = new Deactivated(this);
            this.Card.StatusDate = DateTime.Now;
        }

        public override void Lose()
        {
            // Lose the destroyed card
            this.Card.Status = new Lost(this);
            this.Card.StatusDate = DateTime.Now;
        }

        public override void Extend(DateTime extensionDate)
        {
            // Cannot extend a destroyed card
        }
    }
}
