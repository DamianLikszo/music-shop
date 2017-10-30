using MusicShop.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicShop.Controllers
{
    public class StoreController : Controller
    {
        StoreContext db = new StoreContext();

        // GET: Store
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpNotFoundResult();

            var album = db.Albums.Find(id);

            return View(album);
        }

        public ActionResult List(string genrename)
        {
            if (genrename == null)
                return new HttpNotFoundResult();

            var genre = db.Genres.Include("Albums").Where(g => g.Name.ToUpper() == genrename.ToUpper()).Single();
            var albums = genre.Albums.ToList();

            return View(albums);
        }

        [ChildActionOnly]
        [OutputCache(Duration =  10800)] // 3*60*60
        public ActionResult GenresMenu()
        {
            var genres = db.Genres.ToList();

            return PartialView("_GenresMenu", genres);
        }
    }
}