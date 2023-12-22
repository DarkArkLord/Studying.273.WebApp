using AlchemistStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AlchemistStore.Controllers
{
    [Route("/elements")]
    public class ElementsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("/list")]
        public IActionResult GetList()
        {
            var elements = DependencyResolver.ElementsService.GetAll().ToArray();
            return View(elements);
        }
    }
}
