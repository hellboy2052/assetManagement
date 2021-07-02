using FluentValidation;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.ViewModels.User;
using System;

namespace RookieOnlineAssetManagement.Data.Validatiors
{
    public class PaginationValidator : AbstractValidator<PaginationModel>
    {
        public PaginationValidator ()
        {
            RuleFor(u => u.PageIndex).GreaterThanOrEqualTo(1).WithMessage("Page index must greater than 1");
            RuleFor(u => u.PageSize).GreaterThanOrEqualTo(1).WithMessage("Page size must greater than 1");
        }
    }
}
