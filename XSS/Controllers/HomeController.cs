using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using XSS.Models;

namespace XSS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CommentAdd()
        {
            HttpContext.Response.Cookies.Append("email", "fcakiroglu@outlook.com");
            HttpContext.Response.Cookies.Append("password", "1234");
            if (System.IO.File.Exists("comment.txt"))
            {
                ViewBag.Comment = System.IO.File.ReadAllLines("comment.txt");
            }
            return View();
        }
        [HttpPost]
        public IActionResult CommentAdd(string name,string comment)
        {

            ViewBag.Name = name;
            ViewBag.Comment = comment;

            System.IO.File.AppendAllText("comment.txt", $"{name}--{comment}\n");
            return RedirectToAction("CommentAdd");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
