using MVCandEntityFrameworkPractice.Models.Domain.CardStatuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MVCandEntityFrameworkPractice.Models.Domain
{
    public abstract class CardStatusFactory
    {
        private static Dictionary<string, CardStatus> statusCache = FindAllDerivedStatuses();

        public static CardStatus GetStatus(string statusTypeName)
        {
            return statusCache[statusTypeName];
        }

        private static Dictionary<string, CardStatus> FindAllDerivedStatuses()
        {
            var derivedType = typeof(CardStatus);
            var assembly = Assembly.GetAssembly(typeof(CardStatus));
            return assembly.GetTypes().Where(t => t != derivedType && derivedType.IsAssignableFrom(t))
                        .Select(t => (CardStatus)Activator.CreateInstance(t))
                        .ToDictionary(k => k.DisplayName);
        }
    }
}