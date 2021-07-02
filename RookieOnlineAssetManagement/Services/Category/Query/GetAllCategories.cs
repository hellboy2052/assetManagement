using MediatR;
using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.ViewModels.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Services.CategoryService.Query
{
    public class GetAllCategoriesQuery : IRequest<Result<IEnumerable<CategoryReadModel>>>
    {
    }
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, Result<IEnumerable<CategoryReadModel>>>
    {
        private readonly ApplicationDbContext _context;
        public GetAllCategoriesQueryHandler (ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result<IEnumerable<CategoryReadModel>>> Handle (GetAllCategoriesQuery query, CancellationToken cancellationToken)
        {
            var categoryList = _context.Categories.AsNoTracking();

            var categoryMapped = categoryList.Select(x => new CategoryReadModel
            {
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName,
            });

            return Result<IEnumerable<CategoryReadModel>>.Success(categoryMapped);
        }
    }
}
