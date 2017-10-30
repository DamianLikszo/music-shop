using MusicShop.DAL;
using MusicShop.Infrastructure;
using MusicShop.Models;
using MusicShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicShop.Controllers
{
    public class HomeController : Controller
    {
        private StoreContext db = new StoreContext();

        public ActionResult Index()
        {
            ICacheProvider cache = new DefaultCacheProvider();

            List<Genre> genres;
            if (cache.IsSet(CacheConst.genres))
                genres = cache.Get(CacheConst.genres) as List<Genre>;
            else
            {
                genres = db.Genres.ToList();
                cache.Set(CacheConst.genres, genres, 180);
            }

            List<Album> newArrivals;
            if (cache.IsSet(CacheConst.newArrivals))
                newArrivals = cache.Get(CacheConst.newArrivals) as List<Album>;
            else
            {
                newArrivals = db.Albums.Where(a => !a.isHidden).OrderByDescending(a => a.DateAdded).Take(3).ToList();
                cache.Set(CacheConst.newArrivals, newArrivals, 30);
            }

            List<Album> bestsellers;
            if (cache.IsSet(CacheConst.bestseller))
                bestsellers = cache.Get(CacheConst.bestseller) as List<Album>;
            else
            {
                bestsellers = db.Albums.Where(a => !a.isHidden && a.IsBestseller).OrderBy(g => Guid.NewGuid()).Take(3).ToList();
                cache.Set(CacheConst.bestseller, bestsellers, 15);
            }

            var vm = new HomeViewModel()
            {
                Bestsellers = bestsellers,
                Genres = genres,
                NewArrivals = newArrivals
            };

            return View(vm);
        }

        public ActionResult StaticContent(string viewname)
        {
            return View(viewname);
        }
    }
}