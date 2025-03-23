using MediatR;

namespace StorageAnalyzer.Usecases.Features.Folders.Commands
{
    public class DeleteFolderCommand : IRequest<bool>
    {
        public Guid FolderId { get; set; }
    }
}
