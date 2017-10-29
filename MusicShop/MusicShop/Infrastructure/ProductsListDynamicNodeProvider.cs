using MusicShop.DAL;
using MusicShop.Models;
using MvcSiteMapProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicShop.Infrastructure
{
    public class ProductsListDynamicNodeProvider : DynamicNodeProviderBase
    {
        private StoreContext db = new StoreContext();
        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode node)
        {
            var ListNodes = new List<DynamicNode>();

            foreach (Genre genre in db.Genres)
            {
                DynamicNode myNode = new DynamicNode();
                myNode.Title = genre.Name;
                myNode.Key = "Genre_" + genre.GenreId;
                myNode.RouteValues.Add("genrename", genre.Name);

                ListNodes.Add(myNode);
            }

            return ListNodes;
        }
    }
}