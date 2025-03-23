using MediatR;
using StorageAnalyzer.Shared.DataTransferObjects;

namespace StorageAnalyzer.Usecases.Features.Folders.Queries
{
    public class GetFolderByIdQuery : IRequest<FolderEntityDto?>
    {
        public Guid FolderId { get; set; }
    }
}
