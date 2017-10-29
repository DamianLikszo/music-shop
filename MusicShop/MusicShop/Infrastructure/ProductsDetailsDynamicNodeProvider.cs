using MusicShop.DAL;
using MusicShop.Models;
using MvcSiteMapProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicShop.Infrastructure
{
    public class ProductsDetailsDynamicNodeProvider : DynamicNodeProviderBase
    {
        private StoreContext db = new StoreContext();
        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode node)
        {
            var ListNodes = new List<DynamicNode>();

            foreach (Album album in db.Albums)
            {
                DynamicNode myNode = new DynamicNode();
                myNode.Title = album.AlbumTitle;
                myNode.Key = "Album_" + album.AlbumId;
                myNode.ParentKey = "Genre_" + album.GenreId;
                myNode.RouteValues.Add("id", album.AlbumId);

                ListNodes.Add(myNode);
            }

            return ListNodes;
        }
    }
}