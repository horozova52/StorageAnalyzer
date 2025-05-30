﻿using MediatR;
using StorageAnalyzer.Shared.DataTransferObjects;

namespace StorageAnalyzer.Usecases.Features.Folders.Commands
{
    public class CreateFolderCommand : IRequest<FolderEntityDto>
    {
        public string Path { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
