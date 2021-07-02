using FluentValidation;
using RookieOnlineAssetManagement.ViewModels.User;
using System;

namespace RookieOnlineAssetManagement.Data.Validatiors
{
    public class UserValidators
    {
        public class CreateUserValidator : AbstractValidator<UserCreateModel>
        {
            public CreateUserValidator ()
            {
                RuleFor(u => u.Type).NotEmpty().WithMessage("Please select role");
                RuleFor(u => u.FirstName).NotEmpty().WithMessage("First name can not be empty");
                RuleFor(u => u.LastName).NotEmpty().WithMessage("Last name can not be empty");
                RuleFor(u => u.DoB).Must(AgeGreaterThan18).WithMessage("User is under 18. Please select a different date");
                RuleFor(u => u.JoinedDate)
                    .GreaterThan(u => u.DoB)
                    .WithMessage("Joined date is not later than Date of Birth. Please select a different date")
                    .Must(NotSartudayOrSunday)
                    .WithMessage("Joined date is Saturday or Sunday. Please select a different date");
            }

        }
        public class UpdateUserValidator : AbstractValidator<UserUpdateModel>
        {
            public UpdateUserValidator ()
            {
                RuleFor(u => u.Type).NotEmpty().WithMessage("Please select role");
                RuleFor(u => u.Id).NotEmpty().WithMessage("Empty field must not empty");
                RuleFor(u => u.DateOfBirth).Must(AgeGreaterThan18).WithMessage("User is under 18. Please select a different date");
                RuleFor(u => u.JoinedDate)
                    .GreaterThan(u => u.DateOfBirth)
                    .WithMessage("Joined date is not later than Date of Birth. Please select a different date")
                    .Must(NotSartudayOrSunday)
                    .WithMessage("Joined date is Saturday or Sunday. Please select a different date");
            }
        }

        static bool AgeGreaterThan18 (DateTime dob)
        {
            DateTime Current = DateTime.Today;
            int age = Current.Year - Convert.ToDateTime(dob).Year;

            if (age < 18)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        static bool NotSartudayOrSunday (DateTime joinedDate)
        {
            return (joinedDate.DayOfWeek != DayOfWeek.Sunday && joinedDate.DayOfWeek != DayOfWeek.Saturday);
        }
    }
}
