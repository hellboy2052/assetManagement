using MediatR;
using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Entities;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.Utils;
using RookieOnlineAssetManagement.ViewModels.Category;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Services.CategoryService.Command
{
    public class CreateCategoriesCommand : IRequest<Result<CategoryReadModel>>
    {
        public CategoryCreateModel CreateModel { get; set; }
    }
    public class CreateCategoriesCommandHandler : IRequestHandler<CreateCategoriesCommand, Result<CategoryReadModel>>
    {
        private readonly ApplicationDbContext _context;

        public CreateCategoriesCommandHandler (ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<CategoryReadModel>> Handle (CreateCategoriesCommand command, CancellationToken cancellationToken)
        {
            var existCate = await _context.Categories.AnyAsync(cate => cate.CategoryName == command.CreateModel.CategoryName);

            if (existCate)
            {
                return Result<CategoryReadModel>.Failure($"Category : {command.CreateModel.CategoryName} already exist");
            }

            var generatedCategoryId = Generator.GenerateCategoryId(command.CreateModel.CategoryName);

            var cateToBeAdded = new Category
            {
                CategoryId = generatedCategoryId,
                CategoryName = command.CreateModel.CategoryName,
            };

            try
            {
                await _context.Categories.AddAsync(cateToBeAdded);
                await _context.SaveChangesAsync();

                return Result<CategoryReadModel>.Success(new CategoryReadModel
                {
                    CategoryId = cateToBeAdded.CategoryId,
                    CategoryName = cateToBeAdded.CategoryName,
                });
            }
            catch (Exception ex)
            {
                return Result<CategoryReadModel>.Failure(ex.InnerException.Message);
            }

        }
    }
}
