using System;
using System.Reflection.Metadata;
using DomainLayer.Models;
using Employee_details_webapp.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Interfaces;
using ServiceLayer.Services;
using Microsoft.AspNetCore.Http.Features;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Employee_details_webapp.Controllers
{
    public class CombinedController : Controller
    {

        private readonly IPositionService _positionService;
        private readonly IPeopleService _peopleService;
        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeJobHistoryService _employeeJobHistoryService;
        private IValidator<AddViewModel> _validator;
        private IValidator<EditViewModel> _editValidator;

        public CombinedController(IPositionService positionService, IPeopleService peopleService, IEmployeeService employeeService,
            IEmployeeJobHistoryService employeeJobHistoryService, IValidator<AddViewModel> validator, IValidator<EditViewModel> editValidator)
        {
            _positionService = positionService;
            _peopleService = peopleService;
            _employeeService = employeeService;
            _employeeJobHistoryService = employeeJobHistoryService;
            _validator = validator;
            _editValidator = editValidator;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Combined/AllEmployeesList")]
        public IActionResult AllEmployeesList()
        {
           return View();
        }

        [HttpPost("Combined/Combined/AllEmployeesList2")]
        public JsonResult AllEmployeesList2()
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
                    FullName = people.FirstName + " " + people.MiddleName + " " + people.LastName,
                    Address = people.Address,
                    Email = people.Email,
                    Employeeid = employee.Employeeid,
                    EmployeeCode = employee.EmployeeCode,
                    Salary = employee.Salary,
                    StartDate = employee.StartDate,
                    EndDate = employee.EndDate,
                    ISDisabled = employee.ISDisabled,
                    PositionName = position.PositionName
                };
                combinedViewModelList.Add(combinedViewModel);
            });
            var combinedViewModelList2 = combinedViewModelList.AsQueryable();

            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = combinedViewModelList.Count();
            int filteredRecords = 0;

            //search data when search value found
            if (!string.IsNullOrEmpty(searchValue))
            {
                combinedViewModelList = combinedViewModelList2.Where(x =>
                  x.FirstName.ToLower().Contains(searchValue.ToLower())
                  || x.LastName.ToLower().Contains(searchValue.ToLower())
                  || x.Address.ToLower().Contains(searchValue.ToLower())
                  || x.Salary.ToString().ToLower().Contains(searchValue.ToLower())

                ).ToList();
            }
            combinedViewModelList2 = combinedViewModelList.AsQueryable();

            //filtered data after search
            filteredRecords = combinedViewModelList.Count();

            //sort data
            if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection))
                combinedViewModelList2 = combinedViewModelList2.OrderBy(sortColumn + " " + sortColumnDirection).AsQueryable();


            //pagination
            var empList = combinedViewModelList2.Skip(skip).Take(pageSize).ToList();


            var jsonData = new { draw = draw, recordsFiltered = filteredRecords, recordsTotal = recordsTotal, data = empList };
           // return Ok(jsonData);
            return Json(jsonData);
        }

        [HttpGet]
        public IActionResult AddEmployee()
        {
            ViewBag.positions = _positionService.GetAllPositions().ToList();
            AddViewModel addviewmodel = new()
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.MaxValue
            };
            return View(addviewmodel);
        }

        [HttpPost]
        public IActionResult AddEmployee(AddViewModel addRequest)
        {
            ViewBag.positions = _positionService.GetAllPositions().ToList();
            var people = new People()
            {
                Personid = Guid.NewGuid(),
                FirstName = addRequest.FirstName,
                MiddleName = addRequest.MiddleName,
                LastName = addRequest.LastName,
                Address = addRequest.Address,
                Email = addRequest.Email
            };
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
            var employeeJobHistory = new EmployeeJobHistories()
            {
                EmployeeJobHistoryid = Guid.NewGuid(),
                Employeeid = employee.Employeeid,
                Positionid = addRequest.Positionid,
                StartDate = employee.StartDate,
                EndDate = employee.EndDate
            };

            ValidationResult result = _validator.Validate(addRequest);
            if (result.IsValid)
            {
                _peopleService.InsertPeople(people);
                _employeeService.InsertEmployee(employee);
                _employeeJobHistoryService.InsertEmployeeJobHistory(employeeJobHistory);
                return RedirectToAction("AllEmployeesList");
            }
            else
            {
                //return BadRequest(result.Errors.Select(s => s.ErrorMessage).ToList());
                return View();
            }
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
                EndDate = employee.EndDate,
                OriginalPositionid = employee.Positionid
            };
            return View(editViewModel);
        }

        [HttpPost]
        public IActionResult EditEmployee(EditViewModel editViewModel)
        {
            ViewBag.positions = _positionService.GetAllPositions().ToList();

            var people = new People()
            {
                Personid = editViewModel.Personid,
                FirstName = editViewModel.FirstName,
                MiddleName = editViewModel.MiddleName,
                LastName = editViewModel.LastName,
                Address = editViewModel.Address,
                Email = editViewModel.Email
            };
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
            var employeeJobHistory = new EmployeeJobHistories()
            {
                EmployeeJobHistoryid = Guid.NewGuid(),
                Employeeid = editViewModel.Employeeid,
                Positionid = editViewModel.Positionid,
                StartDate = editViewModel.StartDate,
                EndDate = editViewModel.EndDate
            };
            
            ValidationResult result = _editValidator.Validate(editViewModel);

            if (result.IsValid)
            {
                _peopleService.UpdatePeople(people);
                _employeeService.UpdateEmployee(employee);
                if (editViewModel.Positionid != editViewModel.OriginalPositionid)
                {
                    _employeeJobHistoryService.InsertEmployeeJobHistory(employeeJobHistory);
                }
                return RedirectToAction("AllEmployeesList");
            }
            else
            {
                //return BadRequest(result.Errors.Select(s => s.ErrorMessage).ToList());
                return View(editViewModel);
            }
           

            //return RedirectToAction("AllEmployeesList");
            return Redirect(Url.Action("AllEmployeesList", "Combined") + "");
        }

        [HttpGet("/Combined/EmployeeJobHistoryList/{Id}")]
        public IActionResult EmployeeJobHistoryList(Guid Id)
        {
            var employee = _employeeService.GetEmployee(Id);
            var person = _peopleService.GetPeople(employee.Personid);
            ViewBag.fullname = person.FirstName + " " + person.MiddleName + " " + person.LastName;

            var employeeJobHistoryList = new List<EmployeeJobHistoriesModel>();
            var tempEmployeeJobHistoryList = _employeeJobHistoryService.GetAllEmployeeJobHistories().ToList();

            tempEmployeeJobHistoryList.ForEach(employeeJobHistory =>
            {
                if (employeeJobHistory.Employeeid == Id)
                {
                    var tempPosition = _positionService.GetPosition(employeeJobHistory.Positionid);

                    EmployeeJobHistoriesModel employeeJobHistoryModel = new()
                    {
                        EmployeeJobHistoryid = employeeJobHistory.EmployeeJobHistoryid,
                        Employeeid = employeeJobHistory.Employeeid,
                        Positionid = employeeJobHistory.Positionid,
                        StartDate = DateOnly.FromDateTime(employeeJobHistory.StartDate),
                        EndDate = DateOnly.FromDateTime(employeeJobHistory.EndDate),
                        PositionName = tempPosition.PositionName,
                    };

                    employeeJobHistoryList.Add(employeeJobHistoryModel);
                }
            });
            return View(employeeJobHistoryList);

        }

        [HttpGet("/Combined/EmployeeJobHistoryEdit/{Id}/{Id2}")]
        public IActionResult EmployeeJobHistoryEdit(Guid Id, Guid Id2)
        {
            var employee = _employeeService.GetEmployee(Id);
            var person = _peopleService.GetPeople(employee.Personid);
            var employeeJobHistory = _employeeJobHistoryService.GetEmployeeJobHistory(Id2);

            ViewBag.fullname = person.FirstName + " " + person.MiddleName + " " + person.LastName;
            ViewBag.jobHistory = employeeJobHistory;

            var employeeJobHistoryList = new List<EmployeeJobHistoriesModel>();
            var tempEmployeeJobHistoryList = _employeeJobHistoryService.GetAllEmployeeJobHistories().ToList();

            tempEmployeeJobHistoryList.ForEach(employeeJobHistory =>
            {
                if (employeeJobHistory.Employeeid == Id)
                {
                    var tempPosition = _positionService.GetPosition(employeeJobHistory.Positionid);

                    EmployeeJobHistoriesModel employeeJobHistoryModel = new()
                    {
                        EmployeeJobHistoryid = employeeJobHistory.EmployeeJobHistoryid,
                        Employeeid = employeeJobHistory.Employeeid,
                        Positionid = employeeJobHistory.Positionid,
                        StartDate = DateOnly.FromDateTime(employeeJobHistory.StartDate),
                        EndDate = DateOnly.FromDateTime(employeeJobHistory.EndDate),
                        PositionName = tempPosition.PositionName,
                    };

                    employeeJobHistoryList.Add(employeeJobHistoryModel);
                }
            });

            ViewBag.jobHistoryList = employeeJobHistoryList;

            var tempPosition2 = _positionService.GetPosition(employeeJobHistory.Positionid);
            EmployeeJobHistoriesModeledit employeeJobHistoryModeledit = new()
            {
                EmployeeJobHistoryid = employeeJobHistory.EmployeeJobHistoryid,
                Employeeid = employeeJobHistory.Employeeid,
                Positionid = employeeJobHistory.Positionid,
                StartDate = employeeJobHistory.StartDate,
                EndDate = employeeJobHistory.EndDate,
                PositionName = tempPosition2.PositionName,
            };

            return View(employeeJobHistoryModeledit);
        }

        [HttpPost("/Combined/EmployeeJobHistoryEdit/{Id}/{Id2}")]
        public IActionResult EmployeeJobHistoryEdit(EmployeeJobHistoriesModeledit employeeJobHistoryedit)
        {
            var Id = employeeJobHistoryedit.Employeeid;
            EmployeeJobHistories employeeJobHistories = new()
            {
                EmployeeJobHistoryid = employeeJobHistoryedit.EmployeeJobHistoryid,
                Employeeid = employeeJobHistoryedit.Employeeid,
                Positionid = employeeJobHistoryedit.Positionid,
                StartDate = employeeJobHistoryedit.StartDate,
                EndDate = employeeJobHistoryedit.EndDate
            };
            _employeeJobHistoryService.UpdateEmployeeJobHistory(employeeJobHistories);






            return Redirect(Url.Action("EmployeeJobHistoryList/" + Id, "Combined").Replace("%2F", "/") + "");
        }
    }
}
