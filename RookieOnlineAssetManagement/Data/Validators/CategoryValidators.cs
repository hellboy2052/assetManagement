using FluentValidation;
using RookieOnlineAssetManagement.ViewModels.Category;
using RookieOnlineAssetManagement.ViewModels.User;
using System;

namespace RookieOnlineAssetManagement.Data.Validatiors
{
    public class CategoryValidators
    {

        public class CategoryCreateValidator : AbstractValidator<CategoryCreateModel>
        {
            public CategoryCreateValidator ()
            {
                RuleFor(u => u.CategoryName).MinimumLength(3).WithMessage("Minimum length is 3").NotEmpty().WithMessage("Category name can not be empty");
            }
        }
    }
}
