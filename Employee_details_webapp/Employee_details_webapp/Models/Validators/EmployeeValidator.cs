using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;
using FluentValidation;

namespace Employee_details_webapp.Models.Validators
{
    public class EmployeeValidator : AbstractValidator<AddViewModel>
    {
        public EmployeeValidator()
        {
            RuleFor(p => p.FirstName).NotEmpty().WithMessage("Customer Name should be string").Matches("^[a-zA-Z]+$").WithMessage("Customer Name should be string");
            RuleFor(p => p.Email).EmailAddress().WithMessage("Email must in in email format");
            RuleFor(p => p.Address).NotEmpty().WithMessage("Address is required.");
            RuleFor(p => p.StartDate).NotEmpty().WithMessage("Date of birth is required");

        }
    }
}
