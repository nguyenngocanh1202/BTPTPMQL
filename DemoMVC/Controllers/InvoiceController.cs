using Microsoft.AspNetCore.Mvc;
using DemoMVC.Models;

namespace DemoMVC.Controllers
{
    public class InvoiceController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Invoice model)
        {
            if (ModelState.IsValid)
            {
                model.TotalPrice = GetTotalPrice(model.Quantity, model.UnitPrice);
                string strOutput = $"Số lượng: {model.Quantity} - Đơn giá: {model.UnitPrice:C2} - Tổng tiền: {model.TotalPrice:C2}";
                ViewBag.infoInvoice = strOutput;
            }
            return View(model);
        }

        private decimal GetTotalPrice(int quantity, decimal unitPrice)
        {
            return quantity * unitPrice;
        }
    }
}