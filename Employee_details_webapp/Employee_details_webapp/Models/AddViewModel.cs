using DomainLayer.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Employee_details_webapp.Models
{
	public class AddViewModel
	{
        [Key]
        public int Employeeid { get; set; }
        
        public int Personid { get; set; }
        
        public int Positionid { get; set; }

        public int Salary { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int EmployeeCode { get; set; }
        public Boolean ISDisabled { get; set; } = false;


        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }


    }
}
