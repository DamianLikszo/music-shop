using MvcSiteMapProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicShop.Infrastructure
{
    public class StaticPagesDynamicNodeProvider : DynamicNodeProviderBase
    {
        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode node)
        {
            var ListNodes = new List<DynamicNode>();

            // Key adres Value lang
            var ListPages = new Dictionary<string, string>()
            {
                { "kontakt", "Kontakt" },
                { "onas", "O Nas" },
                { "regulamin", "Regulamin" },
                { "wysylka", "Wysyłka" }
            };

            foreach (KeyValuePair<string, string> page in ListPages)
            {
                DynamicNode myNode = new DynamicNode();
                myNode.Title = page.Value;
                myNode.Key = "StaticPages_" + page.Key;
                myNode.RouteValues.Add("viewname", page.Key);

                ListNodes.Add(myNode);
            }

            return ListNodes;
        }
    }
}