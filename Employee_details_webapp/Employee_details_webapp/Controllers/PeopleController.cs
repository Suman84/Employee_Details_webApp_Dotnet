using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;
using ServiceLayer.Services;

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
        [HttpGet]
        public IActionResult AllPeopleList()
        {
            var data = _peopleService.GetAllPeople().ToList();
            return View(data);
        }

        [HttpGet]
        public IActionResult AddPeople()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPeople(People people)
        {
            if (people != null)
            {
                _peopleService.InsertPeople(people);
                return RedirectToAction("Error");
            }
            return RedirectToAction("Error");
        }
    }
}
