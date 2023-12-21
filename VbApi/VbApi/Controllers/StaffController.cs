using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace VbApi.Controllers;

public class Staff
{
    [Required]
    [StringLength(maximumLength: 250, MinimumLength = 10)]
    public string? Name { get; set; }

    [EmailAddress(ErrorMessage = "Email address is not valid.")]
    public string? Email { get; set; }

    [Phone(ErrorMessage = "Phone is not valid.")]
    public string? Phone { get; set; }

    [Range(minimum: 30, maximum: 400, ErrorMessage = "Hourly salary does not fall within allowed range.")]
    public decimal? HourlySalary { get; set; }
}

public class StaffValidator : AbstractValidator<Staff>
    {
        public StaffValidator()
    {
        RuleFor(staff => staff.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(10, 250).WithMessage("Name must be between 10 and 250 characters.");

        RuleFor(staff => staff.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email address is not valid.");

        RuleFor(staff => staff.Phone)
            .NotEmpty().WithMessage("Phone is required.")
            .Matches(@"^\d{11}$").WithMessage("Phone must be an 11-digit number.");

        RuleFor(staff => staff.HourlySalary)
            .NotEmpty().WithMessage("Hourly salary is required.")
            .GreaterThanOrEqualTo(30).WithMessage("Hourly salary must be greater than or equal to 30.")
            .LessThanOrEqualTo(400).WithMessage("Hourly salary must be less than or equal to 400.");
    }

        private bool BeValidDateOfBirth(DateTime dateOfBirth)
        {
            return dateOfBirth.Date <= DateTime.Today;
        }

        private bool BeGreaterThan21YearsAgo(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year - ((today.Month > dateOfBirth.Month || (today.Month == dateOfBirth.Month && today.Day >= dateOfBirth.Day)) ? 0 : 1);
            return age > 21;
        }
    }



[Route("api/[controller]")]
[ApiController]
public class StaffController : ControllerBase
{
    public StaffController()
    {
    }

    [HttpPost]
    public Staff Post([FromBody] Staff value)
    {
        return value;
    }
}