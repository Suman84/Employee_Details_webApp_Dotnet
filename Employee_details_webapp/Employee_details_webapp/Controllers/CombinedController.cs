using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;

namespace Employee_details_webapp.Controllers
{
    public class CombinedController : Controller
    {
        private readonly IPeopleService _peopleService;
        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeJobHistoryService _employeeJobHistory;

        public CombinedController(//IPeopleService peopleService,
            IEmployeeService employeeService
            //, IEmployeeJobHistoryService employeeJobHistory
            )
        {
            //_peopleService = peopleService;
            _employeeService = employeeService;
            //_employeeJobHistory = employeeJobHistory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AllEmployeesList()
        {
            //var data = _peopleService.GetAllPeople().ToList();
            var data2 = _employeeService.GetAllEmployees().ToList();
            return View(data2);
        }

        [HttpPost]
        public IActionResult AddPeople()
        {

            return View();
        }
    }
}
