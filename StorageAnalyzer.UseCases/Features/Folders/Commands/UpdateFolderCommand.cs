using MediatR;
using StorageAnalyzer.Shared.DataTransferObjects;

namespace StorageAnalyzer.Usecases.Features.Folders.Commands
{
    public class UpdateFolderCommand : IRequest<bool>
    {
        public Guid FolderId { get; set; }
        public string Path { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
