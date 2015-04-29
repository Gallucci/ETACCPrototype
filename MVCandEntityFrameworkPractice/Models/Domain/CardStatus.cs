using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCandEntityFrameworkPractice.Models.Domain
{
    public abstract class CardStatus
    {
        // Properties
        public string DisplayName { get; set; }
        public Card Card { get; set; }

        // Methods
        public abstract void Issue(CardHolder holder, DateTime startDate, DateTime endDate, int pin);
        public abstract void Return();
        public abstract void Destroy();
        public abstract void Deactivate();
        public abstract void Lose();
        public abstract void Extend(DateTime extensionDate);
    }
}