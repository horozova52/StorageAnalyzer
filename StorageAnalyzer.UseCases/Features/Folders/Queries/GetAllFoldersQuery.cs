﻿using MediatR;
using StorageAnalyzer.Shared.DataTransferObjects;

namespace StorageAnalyzer.Usecases.Features.Folders.Queries
{
    public class GetAllFoldersQuery : IRequest<List<FolderEntityDto>>
    {
    }
}
