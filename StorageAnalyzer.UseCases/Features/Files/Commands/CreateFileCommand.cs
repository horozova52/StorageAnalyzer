using MediatR;
using StorageAnalyzer.Core.Entities;
using StorageAnalyzer.Shared.DataTransferObjects;

namespace StorageAnalyzer.UseCases.Files.Commands
{
    public class CreateFileCommand : IRequest<FileEntityDto>
    {
        public string FilePath { get; set; }
        public long SizeInBytes { get; set; }
        public string? Hash { get; set; }
        public DateTime DateModified { get; set; }
        public Guid? FolderId { get; set; }
        public Guid? DuplicateSetId { get; set; }
    }

}
