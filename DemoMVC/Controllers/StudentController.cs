using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
namespace DemoMVC.Controllers
{
    public class StudentdController : Controller
    { 
        public IActionResult Index()
        {
            return View ();
        } 
        

        public string Welcome()
        {
            return "This is the Welcome action method...";
        }
    }
}
