using SolidWaste.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SolidWaste.Controllers
{

    [Authorize(Roles = "Admin")]
    public class MemberController : Controller
    {
        //
        // GET: /Member/
        SolidWasteDb _db = new SolidWasteDb();


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Map()
        {
            ViewBag.menu = "maps";
            return View();
        }

        [HttpGet]
        public ActionResult District(string name, int id)
        {
            ViewBag.id = id;
            ViewBag.menu = "maps";
            //var model =
            //    _db.Municipalities
            //    .Take('*')
            //    .Where(r => r.MunicipalityID == id)
            //    .Select(r => new MunicipalityListViewModel
            //    {
            //        MunicipalityID = r.MunicipalityID,
            //        DistrictID = r.DistrictID,
            //        Name = r.Name
            //    });

            //var model =
            //    from m in _db.Municipalities
            //    where m.DistrictID == id
            //    select new MunicipalityListViewModel
            //    {
            //        MunicipalityID = m.MunicipalityID,
            //        DistrictID = m.DistrictID,
            //        Name = m.Name,
            //        x_coordinate = m.x_coordinate,
            //        y_coordinate = m.y_coordinate,
            //        population = m.population
            //    }; 
            //var model = new MunicipalityListViewModel();
            //model.District = _db.Districts.Single(g =>g.DistrictID == id);
            //var model = _db.Districts.Single(g => g.DistrictID == id);
            ViewBag.District = _db.Districts.Single(g => g.DistrictID == id);

            ViewBag.districtImage =
                (from d in _db.Districts
                where d.DistrictID == id
                select d.image).Single();

            var model =
                (from m in _db.Municipalities
                 where m.DistrictID == id
                 orderby m.Name descending
                 select m);

            ViewBag.numMunicipalities =
                (from m in _db.Municipalities
                 where m.DistrictID == id
                 select m.DistrictID).Count();

            ViewBag.totalPopulation =
                (from m in _db.Municipalities
                 where m.DistrictID == id
                 select m.population).Sum();

            return View(model);
            //return View();
        }

    }
}
