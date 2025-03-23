using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageAnalyzer.UseCases.Features.Files.Commands
{
    public class DeleteFileCommand : IRequest<bool>
    {
        public Guid FileId { get; set; }
    }
}
