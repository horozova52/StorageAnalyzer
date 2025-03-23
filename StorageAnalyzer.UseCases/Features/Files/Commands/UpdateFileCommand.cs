using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageAnalyzer.UseCases.Features.Files.Commands
{
    public class UpdateFileCommand : IRequest<bool>
        {
            public Guid Id { get; set; }
            public string FilePath { get; set; } = string.Empty;
            public long SizeInBytes { get; set; }
            public string Hash { get; set; } = string.Empty;
            public DateTime DateModified { get; set; }
        }
    
}
