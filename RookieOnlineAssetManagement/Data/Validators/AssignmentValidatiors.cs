using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.ViewModels.Assignment;
using RookieOnlineAssetManagement.ViewModels.User;
using System;

namespace RookieOnlineAssetManagement.Data.Validatiors
{
    public class AssignmentValidatiors
    {
        public class AssignmentCreateValidator : AbstractValidator<AssignmentCreateModel>
        {
            public AssignmentCreateValidator ()
            {
                RuleFor(u => u.AssestCode).NotEmpty().WithMessage("Asset can not be empty");
                RuleFor(u => u.UserId).NotEmpty().WithMessage("User can not be empty");
                RuleFor(u => u.AssignedDate)
                    .Must(CurrentOrFutureDate)
                    .WithMessage("Assigned date is only current or future date. Please select a different date");
            }
        }
        public class AssignmentUpdateValidator : AbstractValidator<AssignmentUpdateModel>
        {
            ApplicationDbContext _context;
            public AssignmentUpdateValidator (ApplicationDbContext context)
            {
                _context = context;

                RuleFor(u => u.AssestCode).NotEmpty().WithMessage("Asset can not be empty");
                RuleFor(u => u.UserId).NotEmpty().WithMessage("User can not be empty");

                RuleFor(u => u.AssignedDate)
                    .Must(CurrentOrFutureDate)
                    .WithMessage("Assigned date is only current or future date. Please select a different date");
            }
        }
        public class AssignmentReadEditDetailModelValidator : AbstractValidator<AssignmentReadEditDetailModel>
        {
            public AssignmentReadEditDetailModelValidator ()
            {
                RuleFor(u => u.AssetCode).NotEmpty().WithMessage("Asset code can not be empty");
                RuleFor(u => u.UserId).NotEmpty().WithMessage("User can not be empty");
                RuleFor(u => u.Note).NotEmpty().WithMessage("Note can not be empty");
                RuleFor(u => u.AssignedDate)
                    .Must(CurrentOrFutureDate)
                    .WithMessage("Assigned date is only current or future date. Please select a different date");
            }
        }
        static bool CurrentOrFutureDate (DateTime joinedDate)
        {
            return (joinedDate.Date >= DateTime.Now.Date);
        }
    }
}
