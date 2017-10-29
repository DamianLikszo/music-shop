using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicShop.Infrastructure
{
    public static class UrlHelpers
    {
        public static string GenreIconPath(this UrlHelper helper, string genreIconFileName)
        {
            var genreIconFolder = AppConfig.GenreIconsFolderRelative;
            var path = Path.Combine(genreIconFolder, genreIconFileName);
            var absolutePath = helper.Content(path);

            return absolutePath;
        }

        public static string AlbumCoverPath(this UrlHelper helper, string albumFileName)
        {
            var albumCoverFolder = AppConfig.PhotosFolderRelative;
            var path = Path.Combine(albumCoverFolder, albumFileName);
            var absolutePath = helper.Content(path);

            return absolutePath;
        }
    }
}