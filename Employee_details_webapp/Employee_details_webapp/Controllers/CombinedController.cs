using System.Reflection.Metadata;
using DomainLayer.Models;
using Employee_details_webapp.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;
using ServiceLayer.Services;

namespace Employee_details_webapp.Controllers
{
    public class CombinedController : Controller
    { 

        private readonly IPositionService _positionService;
        private readonly IPeopleService _peopleService;
        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeJobHistoryService _employeeJobHistory;

        public CombinedController(IPositionService positionService, IPeopleService peopleService,IEmployeeService employeeService, IEmployeeJobHistoryService employeeJobHistory)
        {
            _positionService = positionService;
            _peopleService = peopleService;
            _employeeService = employeeService;
            _employeeJobHistory = employeeJobHistory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AllEmployeesList()
        {
            var employees = _employeeService.GetAllEmployees().ToList();
            var people = _peopleService.GetAllPeople().ToList();
            var positions = _positionService.GetAllPositions().ToList();

            List<CombinedViewModel> combinedViewModelList = new List<CombinedViewModel>();

            employees.ForEach(employee =>
            {
                People people = _peopleService.GetPeople(employee.Personid);
                Positions position = _positionService.GetPosition(employee.Positionid);

                CombinedViewModel combinedViewModel = new()
                {
                    
                    FirstName = people.FirstName,
                    MiddleName = people.MiddleName,
                    LastName = people.LastName,
                    Address = people.Address,
                    Email = people.Email,
                    Employeeid = employee.Employeeid,
                    EmployeeCode = employee.EmployeeCode,
                    Salary = employee.Salary,
                    StartDate = DateOnly.FromDateTime(employee.StartDate),
                    EndDate = DateOnly.FromDateTime(employee.EndDate),
                    ISDisabled = employee.ISDisabled,
                    PositionName = position.PositionName
                };
                combinedViewModelList.Add(combinedViewModel);
            });

            return View(combinedViewModelList);
        }

        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddEmployee(AddViewModel addRequest)
        {
            var people = new People()
            {
                Personid = Guid.NewGuid(),
                FirstName = addRequest.FirstName,
                MiddleName = addRequest.MiddleName,
                LastName = addRequest.LastName,
                Address = addRequest.Address,
                Email = addRequest.Email

            };

            _peopleService.InsertPeople(people);
            //return RedirectToAction("AllEmployeesList");

            var position = new Positions()
            {
                Positionid = Guid.NewGuid(),
                PositionName = "developer"
            };

            _positionService.InsertPosition(position);

            var employees = new Employees()
            {
                Employeeid = Guid.NewGuid(),
                EmployeeCode = addRequest.EmployeeCode,
                StartDate = addRequest.StartDate,
                EndDate = addRequest.EndDate,
                Salary = addRequest.Salary,
                Personid = people.Personid,
                Positionid = position.Positionid,
            };
            _employeeService.InsertEmployee(employees);
            return RedirectToAction("AllEmployeesList");

            //return View();
        }
    }
}
