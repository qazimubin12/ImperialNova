using ImperialNova.Services;
using ImperialNova.Entities;
using System.Web.Mvc;

namespace ImperialNova.Controllers
{
    public class LocationController : Controller
    {
        locationservices locationservices = new locationservices();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult LocationIndex()
        {
            var data = locationservices.Getlocations();
            return Json(data);
        }

        [HttpPost]
        public JsonResult Create(Locations Location)
        {
            locationservices.CreateLocations(Location);
            return Json("Location Added");

        }

        public JsonResult Delete(int id)
        {
            locationservices.DeleteLocations(id);
            return Json("Location Removed");
        }

        public JsonResult Edit(int id)
        {
            var data = locationservices.GetLocationsById(id);
            return Json(data);
        }
        [HttpPost]
        public JsonResult Update(Locations Location)
        {
            locationservices.UpdateLocations(Location);
            return Json("Record Updated");
        }
    }
}
