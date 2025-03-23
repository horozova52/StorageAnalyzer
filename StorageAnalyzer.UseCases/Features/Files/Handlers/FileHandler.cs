using MediatR;
using Microsoft.AspNetCore.Http;
using StorageAnalyzer.Infrastructure.Repositories.File;
using StorageAnalyzer.Shared.DataTransferObjects;
using StorageAnalyzer.UseCases.Features.Files.Commands;
using StorageAnalyzer.UseCases.Features.Files.Queries;
using StorageAnalyzer.UseCases.Files.Commands;

namespace StorageAnalyzer.Usecases.Features.Files.Handlers
{
    public class FileHandler :
        IRequestHandler<CreateFileCommand, FileEntityDto>,
        IRequestHandler<UpdateFileCommand, bool>,
        IRequestHandler<DeleteFileCommand, bool>,
        IRequestHandler<GetFileByIdQuery, FileEntityDto?>,
        IRequestHandler<GetAllFileQuery, List<FileEntityDto>>
    {
        private readonly IFileRepository _fileRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FileHandler(IFileRepository fileRepository, IHttpContextAccessor httpContextAccessor)
        {
            _fileRepository = fileRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<FileEntityDto> Handle(CreateFileCommand request, CancellationToken cancellationToken)
        {
            var userId = GetUserIdOrThrow();
            var dto = new FileEntityDto
            {
                FilePath = request.FilePath,
                SizeInBytes = request.SizeInBytes,
                Hash = request.Hash,
                DateModified = request.DateModified,
                UserId = userId
            };
            await _fileRepository.CreateAsync(dto);
            return dto;
        }

        public async Task<bool> Handle(UpdateFileCommand request, CancellationToken cancellationToken)
        {
            var userId = GetUserIdOrThrow();
            var dto = new FileEntityDto
            {
                Id = request.Id,
                FilePath = request.FilePath,
                SizeInBytes = request.SizeInBytes,
                Hash = request.Hash,
                DateModified = request.DateModified,
                UserId = userId
            };
            await _fileRepository.UpdateAsync(dto);
            return true;
        }

        public async Task<bool> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            var userId = GetUserIdOrThrow();
            await _fileRepository.DeleteAsync(request.  FileId, userId);
            return true;
        }

        public async Task<FileEntityDto?> Handle(GetFileByIdQuery request, CancellationToken cancellationToken)
        {
            var userId = GetUserIdOrThrow();
            return await _fileRepository.GetByIdAsync(request.Id, userId);
        }

        public async Task<List<FileEntityDto>> Handle(GetAllFileQuery request, CancellationToken cancellationToken)
        {
            var userId = GetUserIdOrThrow();
            return await _fileRepository.GetAllAsync(userId);
        }

        private string GetUserIdOrThrow()
        {
            var userId = _httpContextAccessor.HttpContext?.User?
                .FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("User ID is missing.");
            return userId;
        }
    }
}
