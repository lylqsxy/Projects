using Cobra.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Cobra.Controllers
{
    public class GeoController : Controller
    {
        // GET: Geo
        public ActionResult Index()
        {
            return View();
        }

        // POST: Geo/DrawFence
        [HttpPost]
        public ActionResult DrawFence(string coordinates, string polygons, string start, string end)
        {
            if (string.IsNullOrEmpty(coordinates) && string.IsNullOrEmpty(polygons) && string.IsNullOrEmpty(start) && string.IsNullOrEmpty(end))
                return Json(new { Result = "Invalid coordinate" }, JsonRequestBehavior.AllowGet); 

            return Json(new { Coordinates = coordinates, Polygons = polygons }, JsonRequestBehavior.AllowGet);
        }

        // GET: Geo/DisplayFence
        public ActionResult DisplayFence()
        {
            return View();
        }

        // GET: Geo/GetLocation
        public ActionResult GetLocation(int id)
        {
            var polygons = new List<Coordinate>()
            {
                new Coordinate (){ Lat= -36.86039455866718f,Lng= 174.7463893890381f}, 
                new Coordinate (){Lat= -36.85352694098278f,Lng= 174.75789070129395f}, 
                new Coordinate(){Lat= -36.860257212361006f, Lng= 174.77574348449707f},
                new Coordinate(){Lat=-36.87550114528901f,Lng=174.77179527282715f},
                new Coordinate(){Lat=-36.88209217421274f,Lng=174.746732711792f}
            };

            var coords = new Coordinate () { Lat= -36.8677858f, Lng= 174.7596694f };
            
            string jsonPolygons = JsonConvert.SerializeObject(polygons); 
            string stringCoords = JsonConvert.SerializeObject(coords); 

            return Json(new { Polygons = jsonPolygons, Location = stringCoords }, JsonRequestBehavior.AllowGet);
        }
    }
}