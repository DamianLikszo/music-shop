using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MusicShop.Infrastructure
{
    public class AppConfig
    {

        private static string _genreIconsFolderRelative = ConfigurationManager.AppSettings["GenreIconFolder"];
        public static string GenreIconsFolderRelative
        {
            get
            {
                return _genreIconsFolderRelative;
            }
        }

        private static string _photosFolderRelative = ConfigurationManager.AppSettings["PhotosFolder"];
        public static string PhotosFolderRelative
        {
            get
            {
                return _photosFolderRelative;
            }
        }
    }
}