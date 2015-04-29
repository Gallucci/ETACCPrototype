using MVCandEntityFrameworkPractice.Models.Domain.CatalogStatuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MVCandEntityFrameworkPractice.Models.Domain
{
    public static class CatalogStatusFactory
    {
        private static Dictionary<string, CatalogStatus> statusCache = FindAllDerivedStatuses();

        public static CatalogStatus GetStatus(string statusTypeName)
        {
            return statusCache[statusTypeName];
        }

        private static Dictionary<string, CatalogStatus> FindAllDerivedStatuses()
        {
            var derivedType = typeof(CatalogStatus);
            var assembly = Assembly.GetAssembly(typeof(CatalogStatus));
            return assembly.GetTypes().Where(t => t != derivedType && derivedType.IsAssignableFrom(t))
                        .Select(t => (CatalogStatus)Activator.CreateInstance(t))
                        .ToDictionary(k => k.DisplayName);
        }
    }
}