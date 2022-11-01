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
        private readonly IEmployeeJobHistoryService _employeeJobHistoryService;

        public CombinedController(IPositionService positionService, IPeopleService peopleService,IEmployeeService employeeService, IEmployeeJobHistoryService employeeJobHistoryService)
        {
            _positionService = positionService;
            _peopleService = peopleService;
            _employeeService = employeeService;
            _employeeJobHistoryService = employeeJobHistoryService;
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
            ViewBag.positions = _positionService.GetAllPositions().ToList();
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


            var employee = new Employees()
            {
                Employeeid = Guid.NewGuid(),
                EmployeeCode = addRequest.EmployeeCode,
                StartDate = addRequest.StartDate,
                EndDate = addRequest.EndDate,
                Salary = addRequest.Salary,
                Personid = people.Personid,
                Positionid = addRequest.Positionid,
            };
            _employeeService.InsertEmployee(employee);

            var employeeJobHistory = new EmployeeJobHistories()
            {
                EmployeeJobHistoryid = Guid.NewGuid(),
                Employeeid = employee.Employeeid,
                Positionid = addRequest.Positionid,
                StartDate = DateOnly.FromDateTime(employee.StartDate),
                EndDate = DateOnly.FromDateTime(employee.EndDate)
            };
            _employeeJobHistoryService.InsertEmployeeJobHistory(employeeJobHistory);

            return RedirectToAction("AllEmployeesList");
        }

        [HttpGet]
        public IActionResult EditEmployee(Guid Id)
        {
            var employee = _employeeService.GetEmployee(Id);
            //var employeeJobHistory = _employeeJobHistoryService.GetEmployeeJobHistory(Id);
            var person = _peopleService.GetPeople(employee.Personid);
            var position = _positionService.GetPosition(employee.Positionid);
            ViewBag.positions = _positionService.GetAllPositions().ToList();

            var editViewModel = new EditViewModel()
            {
                Personid = employee.Personid,
                Employeeid = employee.Employeeid,
                Positionid = employee.Positionid,
                FirstName = person.FirstName,
                MiddleName = person.MiddleName,
                LastName = person.LastName,
                Email = person.Email,
                Address = person.Address,
                Salary = employee.Salary,
                EmployeeCode = employee.EmployeeCode,
                StartDate = employee.StartDate,
                EndDate = employee.EndDate
            };
            return View(editViewModel);
        }

        [HttpPost]
        public IActionResult EditEmployee(EditViewModel editViewModel)
        {

            var people = new People()
            {
                Personid = editViewModel.Personid,
                FirstName = editViewModel.FirstName,
                MiddleName = editViewModel.MiddleName,
                LastName = editViewModel.LastName,
                Address = editViewModel.Address,
                Email = editViewModel.Email
            };
            _peopleService.UpdatePeople(people);


            var employee = new Employees()
            {
                Employeeid = editViewModel.Employeeid,
                EmployeeCode = editViewModel.EmployeeCode,
                StartDate = editViewModel.StartDate,
                EndDate = editViewModel.EndDate,
                Salary = editViewModel.Salary,
                Personid = editViewModel.Personid,
                Positionid = editViewModel.Positionid,
            };
            _employeeService.UpdateEmployee(employee);

            if(editViewModel.Positionid != editViewModel.OriginalPositionid)
            {
                var employeeJobHistory = new EmployeeJobHistories()
                {
                    EmployeeJobHistoryid = Guid.NewGuid(),
                    Employeeid = editViewModel.Employeeid,
                    Positionid = editViewModel.Positionid,
                    StartDate = DateOnly.FromDateTime(editViewModel.StartDate),
                    EndDate = DateOnly.FromDateTime(editViewModel.EndDate)
                };
                _employeeJobHistoryService.InsertEmployeeJobHistory(employeeJobHistory);
            }

            //return RedirectToAction("AllEmployeesList");
            return Redirect(Url.Action("AllEmployeesList", "Combined") + "");
        }


        [HttpGet]
        public IActionResult EmployeeJobHistoryList(Guid Id)
        {
            var employee = _employeeService.GetEmployee(Id);
            var person = _peopleService.GetPeople(employee.Personid);
            ViewBag.fullname = person.FirstName +" "+ person.MiddleName +" "+ person.LastName;

            var employeeJobHistoryList = new List<EmployeeJobHistories>();
            var tempEmployeeJobHistoryList = _employeeJobHistoryService.GetAllEmployeeJobHistories().ToList();

            tempEmployeeJobHistoryList.ForEach(employeeJobHistory =>
            {
                if(employeeJobHistory.Employeeid == Id)
                {
                    employeeJobHistoryList.Add(employeeJobHistory);
                }
            });
            return View(employeeJobHistoryList);

        }

        [HttpGet]
        public IActionResult EmployeeJobHistory(Guid Id)
        {
            var employee = _employeeService.GetEmployee(Id);
            //var employeeJobHistory = _employeeJobHistoryService.GetEmployeeJobHistory(Id);
            var person = _peopleService.GetPeople(employee.Personid);
            var position = _positionService.GetPosition(employee.Positionid);
            //ViewBag.positions = _positionService.GetAllPositions().ToList();
            ViewBag.employeeJobHistory = _employeeJobHistoryService.GetAllEmployeeJobHistories().ToList();


            var employeeJobHistoryModel = new EmployeeJobHistoriesModel()
            {
                EmployeeJobHistoryid = Id,
                Employeeid = employee.Employeeid,
                Positionid = employee.Positionid,
                StartDate = DateOnly.FromDateTime(employee.StartDate),
                EndDate = DateOnly.FromDateTime(employee.EndDate)

            };

            return View();

            var editViewModel = new EditViewModel()
            {
                Personid = employee.Personid,
                Employeeid = employee.Employeeid,
                Positionid = employee.Positionid,
                FirstName = person.FirstName,
                MiddleName = person.MiddleName,
                LastName = person.LastName,
                Email = person.Email,
                Address = person.Address,
                Salary = employee.Salary,
                EmployeeCode = employee.EmployeeCode,
                StartDate = employee.StartDate,
                EndDate = employee.EndDate
            };
            return View(editViewModel);
        }
    }
}
