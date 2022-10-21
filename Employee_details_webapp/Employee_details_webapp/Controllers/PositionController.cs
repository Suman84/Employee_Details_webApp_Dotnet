using DomainLayer.Models;
using Employee_details_webapp.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;
using ServiceLayer.Services;

namespace Employee_details_webapp.Controllers
{
    public class PositionController : Controller
    {

        private readonly IPositionService _positionService;

        public PositionController(IPositionService positionService)
        {
            _positionService = positionService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AllPositionList()
        {
            var data = _positionService.GetAllPositions().ToList();
            return View(data);
        }

        [HttpGet]
        public IActionResult AddPosition()
        {
            ViewBag.data = _positionService.GetAllPositions().ToList();
            return View();
        }

        [HttpPost]
        public IActionResult AddPosition(PositionViewModel positionviewmodel)
        {

            if (positionviewmodel != null)
            {

                Positions positions = new()
                {
                    Positionid = Guid.NewGuid(),
                    PositionName = positionviewmodel.PositionName
                };

                _positionService.InsertPosition(positions);
                return RedirectToAction("AddPosition");
            }
            return RedirectToAction("AddPosition");
        }
    }
}
