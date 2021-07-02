using MediatR;
using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Enums;
using RookieOnlineAssetManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Services.AssetService.Command
{
    public class DeleteAssetCommand : IRequest<Result<Task>>
    {
        public string AssetCode { get; set; }
        public string Location { get; set; }
        public class DeleteAssetCommandHandler : IRequestHandler<DeleteAssetCommand, Result<Task>>
        {
            private readonly ApplicationDbContext _context;

            public DeleteAssetCommandHandler (ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Result<Task>> Handle (DeleteAssetCommand command, CancellationToken cancellationToken)
            {
                var existingAsset = await _context.Assets.FirstOrDefaultAsync(ast => ast.AssestCode == command.AssetCode);

                if (existingAsset == null)
                {
                    return Result<Task>.Failure($"CategoryId : {existingAsset.CategoryId} not exist");
                }

                if (existingAsset.IsAssigned)
                {
                    return Result<Task>.Failure($"Can't delete Asset that already assigned");
                }

                if (existingAsset.Location != command.Location)
                {
                    return Result<Task>.Failure($"{command.Location} User can't delete {existingAsset.Location} Asset");
                }

                try
                {
                    _context.Assets.Remove(existingAsset);
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
}
