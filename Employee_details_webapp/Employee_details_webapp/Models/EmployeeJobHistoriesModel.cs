using DomainLayer.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Employee_details_webapp.Models
{
	public class EmployeeJobHistoriesModel
	{
        [Key]
        public Guid EmployeeJobHistoryid { get; set; }
        public DateOnly StartDate { get; set; } = new DateOnly();
        public DateOnly EndDate { get; set; } = new DateOnly();

        public Guid Employeeid { get; set; }
        public Guid Positionid { get; set; }
        public string PositionName { get; set; }

    }
}
