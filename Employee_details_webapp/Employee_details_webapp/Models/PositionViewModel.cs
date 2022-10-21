using System.ComponentModel.DataAnnotations;

namespace Employee_details_webapp.Models
{
	public class PositionViewModel
	{
        public Guid Positionid { get; set; }
        public string? PositionName { get; set; }

    }
}
