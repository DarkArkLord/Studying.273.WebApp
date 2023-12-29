using DarkLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApiUtils;

namespace DarkLibrary.Controllers
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
            //using (var client = new DarkHttpClient())
            //{
            //    var response = await client.CreateRequest()
            //        .SetUri("https://author_api:8081/Count")
            //        .SendAsync();
            //    string counter = await response.Content.ReadAsStringAsync();
            //}
            return View();
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
