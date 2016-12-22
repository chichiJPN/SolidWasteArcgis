using SolidWaste.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SolidWaste.Controllers
{
    public class AjaxController : Controller
    {
        SolidWasteDb _db = new SolidWasteDb();
        //
        // GET: /Ajax/

        [HttpPost]
        [Authorize]
        public ActionResult GetDistricts()
        {
            var model =
                _db.Districts
                    .Select(r => new
                    {
                        id = r.DistrictID,
                        name = r.Name,
                        x_coor = r.marker_x_coordinate,
                        y_coor = r.marker_y_coordinate
                    });
            return Json(model, JsonRequestBehavior.AllowGet);
        }


        // returns municipalities and district location
        [HttpPost]
        [Authorize]
        public ActionResult GetMunicipalities(int DistrictID)
        {

            var DistrictModel =
                _db.Districts
                    .Where(c => c.DistrictID == DistrictID)
                    .Select(r => new
                    {
                        id = r.DistrictID,
                        name = r.Name,
                        x_coor = r.zoomed_x_coordinate,
                        y_coor = r.zoomed_y_coordinate
                    }).Single();
            return Json(DistrictModel, JsonRequestBehavior.AllowGet);
        }

        // returns municipalities and district location
        [HttpPost]
        [Authorize]
        public ActionResult updateMunicipalityBoundary(int id, string boundaries)
        {
            Municipality municipality = _db.Municipalities.SingleOrDefault(d => d.MunicipalityID == id);

            municipality.boundary = boundaries;

            _db.SaveChanges();

            return null;
        }

        [HttpPost]
        [Authorize]
        public ActionResult updateLorryBoundary(int id, string boundaries)
        {
            Wastelorry wastelorry = _db.Wastelorries.SingleOrDefault(d => d.WastelorryID == id);
            wastelorry.boundary = boundaries;
            _db.SaveChanges();

            return null;
        }


        // returns municipalities and district location
        [HttpPost]
        [Authorize]
        public ActionResult GetLorries(int DistrictID)
        {

            var LorryModel =
                _db.Wastelorries
                    .Where(c => c.DistrictID == DistrictID)
                    .Select(r => new
                    {
                        id = r.WastelorryID,
                        name = r.name,
                        boundaries = r.boundary
                    });
            return Json(LorryModel, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [Authorize]
        public ActionResult addlorry(int districtID, string boundaries, string name)
        {
            Wastelorry wastelorry = new Wastelorry();

            wastelorry.DistrictID = districtID;
            wastelorry.boundary = boundaries;
            wastelorry.name = name;
            _db.Wastelorries.Add(wastelorry);

            _db.SaveChanges();

            int id = wastelorry.WastelorryID;
            return Json(new { id = id });
        }

        // returns municipalities and district location
        [HttpPost]
        [Authorize]
        public ActionResult GetRoutes(int DistrictID)
        {

            var RoutesModel =
                _db.Routes
                    .Where(c => c.DistrictID == DistrictID)
                    .Select(r => new
                    {
                        id = r.RouteID,
                        name = r.name,
                        points = r.points
                    });
            return Json(RoutesModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddRoute(int districtID, string boundaries, string name)
        {
            Route route = new Route();

            route.DistrictID = districtID;
            route.points = boundaries;
            route.name = name;
            _db.Routes.Add(route);

            _db.SaveChanges();

            int id = route.RouteID;
            return Json(new { id = id });
        }

    }
}
