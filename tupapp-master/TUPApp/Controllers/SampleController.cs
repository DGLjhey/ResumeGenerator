using Microsoft.AspNetCore.Mvc;
using TUPApp.Models;

namespace TUPApp.Controllers
{
    public class SampleController : Controller
    {
        public IActionResult Index()
        {
            var vm = new HomeModel
            {
                Title= HomeController.Title,
                Description = HomeController.Description,
                Gender = HomeController.Gender
            };

            return View(vm);
        }
    }
}
