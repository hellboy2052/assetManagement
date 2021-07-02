using MediatR;
using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Services.CategoryService.Command
{
    public class DeleteCategoriesCommand : IRequest<Result<Task>>
    {
        public string Id { get; set; }
    }
    public class DeleteCategoriesCommandHandler : IRequestHandler<DeleteCategoriesCommand, Result<Task>>
    {
        private readonly ApplicationDbContext _context;

        public DeleteCategoriesCommandHandler (ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Task>> Handle (DeleteCategoriesCommand command, CancellationToken cancellationToken)
        {
            var existCate = await _context.Categories.FirstOrDefaultAsync(cate => cate.CategoryId == command.Id);

            if (existCate == null)
            {
                return Result<Task>.Failure($"CategoryId : {existCate.CategoryId} not exist");
            }

            try
            {
                _context.Categories.Remove(existCate);
                await _context.SaveChangesAsync();
                return Result<Task>.Success(Task.CompletedTask);
            }
            catch (Exception ex)
            {
                return Result<Task>.Failure(ex.InnerException.Message);
            }

        }
    }
}
