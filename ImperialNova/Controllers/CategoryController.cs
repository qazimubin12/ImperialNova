using ImperialNova.Entities;
using ImperialNova.Services;
using System.Web.Mvc;

namespace ImperialNova.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        CategoryServices CategoryServices = new CategoryServices();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult CategoryIndex()
        {
            var data = CategoryServices.GetCategorys();
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Create(Category Category)
        {
            CategoryServices.CreateCategory(Category);
            return new JsonResult();
          

        }

        public JsonResult Delete(int id)
        {
            CategoryServices.DeleteCategory(id);
            return new JsonResult();
        }

        public JsonResult Edit(int id)
        {
            var data = CategoryServices.GetCategoryById(id);
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Update(Category Category)
        {
            CategoryServices.UpdateCategory(Category);
            return new JsonResult();
        }
    }
}
