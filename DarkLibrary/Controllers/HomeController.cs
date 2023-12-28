using DarkLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DarkLibrary.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        private async Task<string> SendRequest()
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                // Call *mywebapi*, and display its response in the page
                var request = new System.Net.Http.HttpRequestMessage();
                // webapi is the container name
                request.RequestUri = new Uri("http://authorapi/Count");
                var response = await client.SendAsync(request);
                string counter = await response.Content.ReadAsStringAsync();
                return counter;
            }
        }

        public IActionResult Index()
        {
            string? res = null; //SendRequest().Result;
            return View("Index", res);
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
