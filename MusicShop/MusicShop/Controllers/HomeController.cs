using MusicShop.DAL;
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

        // GET: Home
        public ActionResult Index()
        {
            var genres = db.Genres.ToList();
            var newArrivals = db.Albums.Where(a => !a.isHidden).OrderByDescending(a => a.DateAdded).Take(3).ToList();
            var bestsellers = db.Albums.Where(a => !a.isHidden && a.IsBestseller).OrderBy(g => Guid.NewGuid()).Take(3).ToList();
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