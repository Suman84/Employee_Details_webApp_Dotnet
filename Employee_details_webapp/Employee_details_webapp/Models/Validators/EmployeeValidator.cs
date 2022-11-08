using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;

namespace Employee_details_webapp.Models.Validators
{
    public class EmployeeValidator : AbstractValidator<AddViewModel>
    {

        public EmployeeValidator()
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

            When(p => p.EmailList.Contains(p.Email), () =>
            {
                RuleFor(p => p.Email).Null()
                .WithMessage("Email already exists");
            });

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
        private bool IsDuplicate(AddViewModel r)
        {
            return r.EmailList.Contains(r.Email);
        }

    }
}