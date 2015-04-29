using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCandEntityFrameworkPractice.Models.Domain.CardStatuses
{
    public class InStock : CardStatus
    {
        public InStock() 
        {
            Initialize();
        }

        public InStock(Card card) 
        {
            Initialize();
            this.Card = card;
        }

        public InStock(CardStatus status)
        {
            Initialize();
            this.Card = status.Card;
        }

        private void Initialize()
        {
            DisplayName = "In Stock";
        }

        public override void Issue(CardHolder holder, DateTime startDate, DateTime endDate, int pin)
        {
            // Issue the in stock card
            this.Card.Holder = holder;
            this.Card.StartDate = startDate;
            this.Card.EndDate = endDate;
            this.Card.Pin = pin;
            this.Card.AccessLevels = this.Card.Catalog.AccessLevels;
            
            this.Card.Status = new Issued(this);
            this.Card.StatusDate = DateTime.Now;    
        }

        public override void Return()
        {
            // Can't return a card already in stock
        }

        public override void Destroy()
        {
            // Destroy the in stock card
            this.Card.Status = new Destroyed(this);
            this.Card.StatusDate = DateTime.Now;
        }

        public override void Deactivate()
        {
            // Destroy the in stock card
            this.Card.Status = new Deactivated(this);
            this.Card.StatusDate = DateTime.Now;
        }

        public override void Lose()
        {
            // Lose the in stock card
            this.Card.Status = new Lost(this);
            this.Card.StatusDate = DateTime.Now;
        }

        public override void Extend(DateTime extensionDate)
        {
            // Cannot extend a card that's in stock
        }
    }
}