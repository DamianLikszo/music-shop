﻿using MusicShop.DAL;
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

        public ActionResult List(string genrename, string searchQuery = null)
        {
            if (genrename == null)
                return new HttpNotFoundResult();

            var genre = db.Genres.Include("Albums").Where(g => g.Name.ToUpper() == genrename.ToUpper()).Single();
            var albums = genre.Albums.Where(a => (searchQuery == null ||
                                                  a.AlbumTitle.ToLower().Contains(searchQuery.ToLower()) ||
                                                  a.ArtistName.ToLower().Contains(searchQuery.ToLower())) &&
                                                  !a.isHidden).ToList();

            if( (Request.IsAjaxRequest()) )
            {
                return PartialView("_ProductsList", albums);
            }

            return View(albums);
        }

        [ChildActionOnly]
        [OutputCache(Duration =  10800)] // 3*60*60
        public ActionResult GenresMenu()
        {
            var genres = db.Genres.ToList();

            return PartialView("_GenresMenu", genres);
        }

        public ActionResult AlbumsSuggestions(string term)
        {
            var albums = this.db.Albums.Where(a => !a.isHidden && a.AlbumTitle.ToLower().Contains(term.ToLower())).Take(5).
                            Select( a => new { label = a.AlbumTitle } );

            return Json(albums, JsonRequestBehavior.AllowGet);
        }
    }
}