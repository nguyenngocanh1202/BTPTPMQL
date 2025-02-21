using Microsoft.AspNetCore.Mvc;
using DemoMVC.Models;

namespace DemoMVC.Controllers
{
    public class SubjectScoreController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(SubjectScore model)
        {
            if (ModelState.IsValid)
            {
                model.FinalScore = GetFinalScore(model.ScoreA, model.ScoreB, model.ScoreC);
                string strOutput = $"Điểm thành phần A: {model.ScoreA} - Điểm thành phần B: {model.ScoreB} - Điểm thành phần C: {model.ScoreC} - Điểm cuối cùng: {model.FinalScore:F2}";
                ViewBag.infoScore = strOutput;
            }
            return View(model);
        }

        private float GetFinalScore(float scoreA, float scoreB, float scoreC)
        {
            return (scoreA*0.6f) + (scoreB*0.3f) + (scoreC*0.1f);
        }
    }
}