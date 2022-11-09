using DomainLayer.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Employee_details_webapp.Models
{
	public class CombinedViewModel
	{
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }

        public string FullName { get; set; }
        public Guid Employeeid { get; set; }
        public int Salary { get; set; } = 0;
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int EmployeeCode { get; set; } = 0;
        public Boolean ISDisabled { get; set; } = false;
        public Guid Personid { get; set; }
        public Guid Positionid { get; set; }

        public string? PositionName { get; set; }

    }
}
