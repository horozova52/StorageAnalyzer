//using MediatR;
//using StorageAnalyzer.Core.Entities;
//using StorageAnalyzer.Infrastructure.Contexts;
//using StorageAnalyzer.UseCases.Files.Commands;

//namespace StorageAnalyzer.UseCases.Files.Handlers
//{
//    public class CreateFileHandler : IRequestHandler<CreateFileCommand, FileEntity>
//    {
//        private readonly ApplicationDbContext _dbContext;

//        public CreateFileHandler(ApplicationDbContext dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        public async Task<FileEntity> Handle(CreateFileCommand request, CancellationToken cancellationToken)
//        {
//            var file = new FileEntity
//            {
//                Id = Guid.NewGuid(),
//                FilePath = request.FilePath,
//                SizeInBytes = request.SizeInBytes,
//                Hash = request.Hash,
//                DateModified = request.DateModified,
//                FolderId = request.FolderId
//            };

//            _dbContext.Files.Add(file);
//            await _dbContext.SaveChangesAsync(cancellationToken);

//            return file;
//        }
//    }
//}
