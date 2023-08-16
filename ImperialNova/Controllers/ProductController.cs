using ImperialNova.Entities;
using ImperialNova.Services;

using System;
using System.Web.Mvc;

namespace ImperialNova.Controllers
{

    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        ProductServices ProductServices = new ProductServices();
        CategoryServices CategoryServices = new CategoryServices();
        public ActionResult Index()
        {
            var categories = CategoryServices.GetCategorys();
            ViewBag.Categories = categories;

            return View();
        }
        public JsonResult CategoryIndex()
        {
            var categories = CategoryServices.GetCategorys();
            return Json(categories);
        }
        public JsonResult ProductIndex()
        {
            var data = ProductServices.GetProducts();
            return Json(data);
        }
        public JsonResult ProductExport(DateTime startingDate, DateTime endingDate)
        {
            var data = ProductServices.ExportProducts(startingDate, endingDate);
            return Json(data);
        }
        [HttpPost]
        public JsonResult Create(Product Product)
        {
            ProductServices.CreateProduct(Product);
            return Json("Product Added");

        }

        public JsonResult Delete(int id)
        {
            ProductServices.DeleteProduct(id);
            return Json("Product Removed");
        }

        public JsonResult Edit(int id)
        {
            var data = ProductServices.GetProductById(id);
            var data2 = CategoryServices.GetCategoryById(data._CategoryId);
            var result = new { ProductData = data, CategoryData = data2 };
            return Json(result);
        }
        [HttpPost]
        public JsonResult Update(Product Product)
        {
            ProductServices.UpdateProduct(Product);
            return Json("Record Updated");
        }
    }
}
