using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using DemoMVC.Models;
namespace DemoMVC.Controllers
{
    public class PersonController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Person ps)
        {
            string strOutput = "Căn cước công dân: " + ps.PersonId + " Họ tên: " + ps.FullName + " Địa chỉ: " + ps.Address;
            ViewBag.infoPerson = strOutput;
            return View();
        }
     
    }
}