using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeakPals_BDD_Tests.Shared
{
    // Sitewide definitions and useful methods
    public class Common
    {
        public const string BaseUrl = "http://localhost:5044";     // copied from launchSettings.json
        

        // File to store browser cookies in
         public const string CookieFile = "..\\..\\..\\..\\..\\StandupsCookies.txt";

        // Page names that everyone should use
        // A handy way to look these up
        public static readonly Dictionary<string, string> Paths = new()
        {
            { "Home" , "/" },
            { "Report", "/Report/Report" },
            { "Test", "/Report/Test" },
            { "Record", "/Record/Record" },
            { "Login", "/Identity/Account/Login" },
            { "Register", "/Identity/Account/Register" },
            { "Manage", "/Identity/Account/Manage" },
            { "Logout", "/Identity/Account/Logout" },
            { "Privacy", "/Privacy" },
            { "Error", "/Home/Error" },
            { "NotFound", "/Home/NotFound" },
            { "ServerError", "/Home/ServerError" },
            { "Search", "/locations/search" },
            { "Area", "/locations/areas" },
            { "Climb", "/locations/climbs" },
            { "Profile", "/Profile"},
            { "Community", "/Community"}
        };

        public static string PathFor(string pathName) => Paths[pathName];
        public static string UrlFor(string pathName) => BaseUrl + Paths[pathName];
        public static string UrlForArea(string areaId) => BaseUrl + Paths["Area"] + "/" + areaId;
        public static string UrlForClimb(string climbId) => BaseUrl + Paths["Climb"] + "/" + climbId;

    }
}