using ImperialNova.Database;
using ImperialNova.Entities;
using ImperialNova.Models;
using ImperialNova.Services;
using Microsoft.Build.Framework;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ImperialNova.Controllers
{
    public class DeliveryFormController : Controller
    {
        DeliveryFormServices DeliveryFormServices = new DeliveryFormServices();
        private readonly IEmailSender _emailSender;
        private readonly DSContext _context;
        //private readonly ILogger<DeliveryFormController> _logger;
        public DeliveryFormController(/*ILogger<DeliveryFormController> logger,*/ DSContext context, IEmailSender emailSender)
        {
            //_logger = logger;
            _context = context;
            _emailSender = emailSender;
        }
        public ActionResult Index()
        {
            return View();
        }
        static DeliveryForm data2;
        List<DeliveryFormProductsDB> _Products = new List<DeliveryFormProductsDB>();
        [HttpPost]
        public ActionResult SubmitDeliveryForm(DeliveryFormModel form)
        {
            var data = form;
            var deliveryForm = new DeliveryForm()
            {
                _SlaesPerson = form._SlaesPerson,
                _Date = form._Date,
                _CustomerName = form._CustomerName,
                _Address = form._Address,
                _Country = form._Country,
                _ContactNo = form._ContactNo,
                _Email = form._Email,
                _Note = form._Note,
                _RequestedDate = form._RequestedDate,
                _TotalAmount = form._TotalAmount,
                _PayMethod = form._PayMethod,
                _AmountPaid = form._AmountPaid,
                _AmountInBalance = form._AmountInBalance,
                ProductsData = JsonConvert.SerializeObject(form._Products)

        };
            DeliveryFormServices.CreateDeliveryForm(deliveryForm);
            

            //// Add products from the form to the data2
            //foreach (var item in form._Products)
            //{
            //    _Products.Add(new DeliveryFormProductsDB
            //    {
            //        _ProductName = item._ProductName,
            //        _ProductQuantity = item._ProductQuantity
            //    });
            //}
            
            //deliveryForm.ProductsData = JsonConvert.SerializeObject(form._Products);
            //DeliveryFormServices.UpdateDeliveryForm(deliveryForm);
            SendEmail(deliveryForm,_Products);
            SendEmailCustomer(deliveryForm, _Products);
            return Json(new { success = true, _id = deliveryForm._id });

        }
        private async Task SendEmail(DeliveryForm formdata, List<DeliveryFormProductsDB> products)
        {
            var userEmail = "qazimubin444@gmail.com"; // Replace with the recipient's email address
            var subject = "Delivery Form";
            var body = "Salesperson: " + formdata._SlaesPerson + "\n" +
                       "Date: " + formdata._Date + "\n" +
                       "Customer Name: " + formdata._CustomerName + "\n" +
                       "Address: " + formdata._Address + "\n" +
                       "Country: " + formdata._Country + "\n" +
                       "Contact No: " + formdata._ContactNo + "\n" +
                       "Email: " + formdata._Email + "\n" +
                       "Note: " + formdata._Note + "\n" +
                       "Requested Date: " + formdata._RequestedDate + "\n" +
                       "Total Amount: " + formdata._TotalAmount + "\n" +
                       "Payment Method: " + formdata._PayMethod + "\n" +
                       "Amount Paid: " + formdata._AmountPaid + "\n" +
                       "Amount In Balance: " + formdata._AmountInBalance + "\n";

            foreach (var product in products)
            {
                body += "Product Name: " + product._ProductName + " Quantity: " + product._ProductQuantity + "\n";
            }


            await _emailSender.SendEmailAsync(userEmail, subject, body);

            
        }
        private async Task SendEmailCustomer(DeliveryForm formdata, List<DeliveryFormProductsDB> products)
        {
            var userEmail = formdata._Email; // Customer Email
            var subject = "Delivery Form";
            var body = "Salesperson: " + formdata._SlaesPerson + "\n" +
                       "Date: " + formdata._Date + "\n" +
                       "Customer Name: " + formdata._CustomerName + "\n" +
                       "Address: " + formdata._Address + "\n" +
                       "Country: " + formdata._Country + "\n" +
                       "Contact No: " + formdata._ContactNo + "\n" +
                       "Email: " + formdata._Email + "\n" +
                       "Note: " + formdata._Note + "\n" +
                       "Requested Date: " + formdata._RequestedDate + "\n" +
                       "Total Amount: " + formdata._TotalAmount + "/-\n" +
                       "Payment Method: " + formdata._PayMethod + "/-\n" +
                       "Amount Paid: " + formdata._AmountPaid + "/-\n" +
                       "Amount In Balance: " + formdata._AmountInBalance + "\n";

            foreach (var product in products)
            {
                body += "Product Name: " + product._ProductName + " Quantity: " + product._ProductQuantity + "\n";
            }


            await _emailSender.SendEmailAsync(userEmail, subject, body);


        }
        public ActionResult Invoice(int id)
        {

            var data = DeliveryFormServices.GetFormById(id);
            var deliveryForm = new DeliveryFormModel()
            {
                _id = id,
                _SlaesPerson = data._SlaesPerson,
                _Date = data._Date,
                _CustomerName = data._CustomerName,
                _Address = data._Address,
                _Country = data._Country,
                _ContactNo = data._ContactNo,
                _Email = data._Email,
                _Note = data._Note,
                _RequestedDate = data._RequestedDate,
                _TotalAmount = data._TotalAmount,
                _PayMethod = data._PayMethod,
                _AmountPaid = data._AmountPaid,
                _AmountInBalance = data._AmountInBalance

            };
            deliveryForm.Products = JsonConvert.DeserializeObject<List<ProductData>>(data.ProductsData);
            //Yaha Products Ni arhe
            //foreach (var item in data2._Products)
            //{
            //    deliveryForm._Products.Add(new DeliveryFormProducts
            //    {
            //        _ProductName = item._ProductName,

            //        _ProductQuantity = item._ProductQuantity
            //    });
            //}
            //// Ya

            return View(deliveryForm);
         
        }
        

    }






    
}
