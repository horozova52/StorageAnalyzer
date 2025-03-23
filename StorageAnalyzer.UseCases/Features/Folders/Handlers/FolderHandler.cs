using MediatR;
using Microsoft.AspNetCore.Http;
using StorageAnalyzer.Infrastructure.Repositories.Folder;
using StorageAnalyzer.Usecases.Features.Folders.Commands;
using StorageAnalyzer.Usecases.Features.Folders.Queries;
using StorageAnalyzer.Shared.DataTransferObjects;
using System.Security.Claims;

namespace StorageAnalyzer.Usecases.Features.Folders.Handlers
{
    public class FolderHandler :
        IRequestHandler<CreateFolderCommand, FolderEntityDto>,
        IRequestHandler<UpdateFolderCommand, bool>,
        IRequestHandler<DeleteFolderCommand, bool>,
        IRequestHandler<GetFolderByIdQuery, FolderEntityDto?>,
        IRequestHandler<GetAllFoldersQuery, List<FolderEntityDto>>
    {
        private readonly IFolderRepository _folderRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FolderHandler(
            IFolderRepository folderRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _folderRepository = folderRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        // Create
        public async Task<FolderEntityDto> Handle(CreateFolderCommand request, CancellationToken cancellationToken)
        {
            var userId = GetUserId();
            var folderDto = new FolderEntityDto
            {
                Path = request.Path,
                Name = request.Name,
                UserId = userId
            };

            var newId = await _folderRepository.CreateAsync(folderDto);
            folderDto.Id = newId;
            return folderDto;
        }

        // Update
        public async Task<bool> Handle(UpdateFolderCommand request, CancellationToken cancellationToken)
        {
            var userId = GetUserId();
            var folderDto = new FolderEntityDto
            {
                Id = request.FolderId,
                Path = request.Path,
                Name = request.Name,
                UserId = userId
            };
            await _folderRepository.UpdateAsync(folderDto);
            return true;
        }

        // Delete
        public async Task<bool> Handle(DeleteFolderCommand request, CancellationToken cancellationToken)
        {
            var userId = GetUserId();
            await _folderRepository.DeleteAsync(request.FolderId, userId);
            return true;
        }

        // Get by ID
        public async Task<FolderEntityDto?> Handle(GetFolderByIdQuery request, CancellationToken cancellationToken)
        {
            var userId = GetUserId();
            return await _folderRepository.GetByIdAsync(request.FolderId, userId);
        }

        // Get all
        public async Task<List<FolderEntityDto>> Handle(GetAllFoldersQuery request, CancellationToken cancellationToken)
        {
            var userId = GetUserId();
            return await _folderRepository.GetAllAsync(userId);
        }

        private string GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext?.User?
                         .FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("User ID is missing.");
            return userId;
        }
    }
}
