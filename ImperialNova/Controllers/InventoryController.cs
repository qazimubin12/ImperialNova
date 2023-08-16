using ImperialNova.Database;
using ImperialNova.Entities;
using ImperialNova.Services;
using Microsoft.CodeAnalysis;
using System.Data;
using System;
using System.Security.Claims;
using System.Web.Mvc;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.IO;
using ClosedXML.Excel;
using System.Linq;

namespace ImperialNova.Controllers
{

    [Authorize]
    public class InventoryController : Controller
    {
        ProductServices ProductServices = new ProductServices();
        InventoryServices InventoryServices = new InventoryServices();
        List<Inventory> productListWithQuantities = new List<Inventory>();
        InventoryBackupsServices InventoryBackupsServices = new InventoryBackupsServices();
        InventoryTemporaryServices InventoryTemporaryServices = new InventoryTemporaryServices();
        locationservices locationservices = new locationservices();
        private readonly UserManager<User> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly DSContext _context;
        //private readonly ILogger<InventoryController> _logger;

        public InventoryController()
        {

        }
        public InventoryController(UserManager<User> userManager,DSContext context, IEmailSender emailSender)
        {
            _userManager = userManager;
            _context = context;
            _emailSender = emailSender;
        }
        public ActionResult Index()
        {
            var products = ProductServices.GetProducts();
            ViewBag.Products = products;
            ViewBag.UserId = User.Identity.GetUserId();
            var locations = locationservices.Getlocations();
            ViewBag.Locations = locations;
            CheckProductQuantities();
            return View();
        }
        public void CheckProductQuantities()
        {
            List<Product> emailproducts = new List<Product>();
            var products = ProductServices.GetProducts();
            foreach (var product in products)
            {
                if (product._Quantity <= 25)
                {                 
                    emailproducts.Add(product);
                }
            }
            SendEmail(emailproducts);
        }
        private async Task SendEmail(List<Product> products)
        {
            foreach (var product in products)
            {

                var userEmail = "qazimubin444@gmail.com"; // Replace with the recipient's email address
                var subject = "Low Quantity Alert: " + product._Name + " quantity is below 25";
                var body = "Order " + product._Name + " stock before it runs out!";

                // Clone the memory stream to a new memory stream to prevent it from being closed


                await _emailSender.SendEmailAsync(userEmail, subject, body);

            }          
           
        }
        public async Task<ActionResult> ExportProductInExcelAndEmail()
        {
            var inventory = await InventoryTemporaryServices.GetInventoriesAsync();
            var fileName = "Stock Update.xlsx";
            var memoryStream = GenerateExcel(inventory);
            await SendEmailWithAttachment(fileName, memoryStream);
            InventoryTemporaryServices.Empty();
            return RedirectToAction("Index");
        }

        private MemoryStream GenerateExcel(IEnumerable<InventoryTemporary> Inventory)
        {
            DataTable dataTable = new DataTable("InventoryTemporary");
            dataTable.Columns.AddRange(new DataColumn[]
            {
            new DataColumn("Action"),
            new DataColumn("Product"),
            new DataColumn("Size"),
            new DataColumn("Color"),
            new DataColumn("Quantity In"),
            new DataColumn("Quantity Out"),
            new DataColumn("Quantity Available"),
            new DataColumn("Employee Name"),
            new DataColumn("Employee Email"),
            new DataColumn("Date & Time")
            });

            foreach (var products in Inventory)
            {
                dataTable.Rows.Add(products._Action, products._Name,products._Size, products._Color, products._QuantityIn,products._QuantityOut, products._Quantity,products._UserFullName,products._UserEmail,products._ExportDate);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return stream;
                }
            }
        }
        private async Task SendEmailWithAttachment(string fileName, MemoryStream memoryStream)
        {
            var userEmail = "qazimubin444@gmail.com"; // Replace with the recipient's email address
            var subject = "Stock Updated - " + locationselected;
            var body = "Please find the attached Excel file containing data.";

            // Clone the memory stream to a new memory stream to prevent it from being closed
            using (var emailStream = new MemoryStream(memoryStream.ToArray()))
            {
                // Set the position of the emailStream back to the beginning
                emailStream.Position = 0;

                await _emailSender.SendEmailAsync(userEmail, subject, body, emailStream, fileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
        }
        public JsonResult GetUserById(string id)
        {
            using (var context = new DSContext())
            {
                // Assuming your 'User' class represents the user entity in your database
                User user = context.Users.FirstOrDefault(u => u.Id == id);
                return Json(user?.Name); // Return the Name property if the user is found, or null if not found

            }
        }
        public JsonResult GetUserByIdEmail(string id)
        {
            using (var context = new DSContext())
            {
                // Assuming your 'User' class represents the user entity in your database
                User user = context.Users.FirstOrDefault(u => u.Id == id);
                return  Json(user?.Email); // Return the Name property if the user is found, or null if not found
            }
        }
        static public string locationselected; 
        public JsonResult AddStock(List<Inventory> selectedProducts, string selectedLocation)
        {
            locationselected = selectedLocation;
            foreach (var product in selectedProducts)
            {
                var data = ProductServices.GetProductById(product._ProductId);


                productListWithQuantities.Add(new Inventory
                {
                    _ProductId = product._ProductId,
                    _ProductName = data._Name,
                    _ToBeChangedQuantity = product._ToBeChangedQuantity
                });
            }

            foreach (var product in productListWithQuantities)
            {
                InventoryServices.CreateInventory(product);
            }

            //ExportProductInExcelAndEmail();
            return Json("Added");
        }

        public JsonResult Create( InventoryBackups inventoryBackups)
        {           

            InventoryBackupsServices.CreateInventoryBackup(inventoryBackups);
            
            return  Json("Inventory Added");
        }
        public JsonResult CreateTemp(InventoryTemporary inventoryTemporary)
        {

            InventoryTemporaryServices.CreateInventoryTemporary(inventoryTemporary);
            ExportProductInExcelAndEmail();            
            return Json("Inventory Added");
        }
        
    }
}
