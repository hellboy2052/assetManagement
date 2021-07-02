using FluentValidation;
using RookieOnlineAssetManagement.ViewModels.Asset;
using System;

namespace RookieOnlineAssetManagement.Data.Validatiors
{
    public class AssetValidators
    {

        public class CeateAssetValidator : AbstractValidator<AssetCreateModel>
        {
            public CeateAssetValidator ()
            {
                RuleFor(u => u.AssestName).NotEmpty().WithMessage("{PropertyName} is required.");
                RuleFor(u => u.Specification).NotEmpty().WithMessage("{PropertyName} is required.");
                RuleFor(u => u.CategoryId).NotEmpty().WithMessage("{PropertyName} is required.");
                RuleFor(u => u.InstallDate).NotEmpty().WithMessage("{PropertyName} is required.");
            }
        }
    }
}
