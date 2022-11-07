using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;


namespace Employee_details_webapp.Models.Validators
{
    public class EditEmployeeValidator : AbstractValidator<EditViewModel>
    {

        public EditEmployeeValidator()
        {

            RuleFor(p => p.FirstName).NotEmpty()
                .WithMessage("First Name cannot be empty")
                .Matches("^[a-zA-Z]+$")
                .WithMessage("First Name can only contain letters");

            RuleFor(p => p.MiddleName).Matches("^[a-zA-Z]+$")
                .WithMessage("Middle Name can only contain letters");

            RuleFor(p => p.LastName).NotEmpty()
                .WithMessage("Last Name cannot be empty")
                .Matches("^[a-zA-Z]+$")
                .WithMessage("Last Name can only contain letters");

            RuleFor(p => p.Email).NotEmpty()
                .WithMessage("Email cannot be empty")
                .EmailAddress()
                .WithMessage("Email must in in email format");

            RuleFor(p => p.Address).NotEmpty()
                .WithMessage("Address cannot be empty.");

            RuleFor(p => p.Salary).NotEmpty()
                .WithMessage("Salary cannot be empty.");

            RuleFor(p => p.EmployeeCode).NotEmpty()
                .WithMessage("Employee Code cannot be empty.");

            RuleFor(p => p.Positionid).NotEqual(Guid.Parse("00000000-0000-0000-0000-000000000000"))
                .WithMessage("Position has to be choosen");

        }

    }
}
