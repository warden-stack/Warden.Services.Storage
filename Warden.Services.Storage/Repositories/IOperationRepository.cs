﻿using System;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Operations.Shared.Dto;

namespace Warden.Services.Storage.Repositories
{
    public interface IOperationRepository
    {
        Task<Maybe<OperationDto>> GetAsync(Guid requestId);
        Task AddAsync(OperationDto operation);
        Task UpdateAsync(OperationDto operation);
    }
}