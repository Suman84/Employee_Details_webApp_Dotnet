using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;

namespace Employee_details_webapp.Controllers
{
    public class PeopleController : Controller
    {

        private readonly IPeopleService _peopleService;

        public PeopleController(IPeopleService peopleService)
        {
            _peopleService = peopleService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AllPeopleList()
        {
            var data = _peopleService.GetAllPeople().ToList();
            return View(data);
        }

        [HttpPost]
        public IActionResult AddPeople()
        {

            return View();
        }
    }
}
