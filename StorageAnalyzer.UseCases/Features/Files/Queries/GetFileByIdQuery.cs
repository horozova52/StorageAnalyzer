﻿using MediatR;
using StorageAnalyzer.Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageAnalyzer.UseCases.Features.Files.Queries
{
        public class GetFileByIdQuery : IRequest<FileEntityDto>
    {
        public Guid Id { get; set; }
    }
}
