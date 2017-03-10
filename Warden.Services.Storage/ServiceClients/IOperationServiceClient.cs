﻿using System;
using System.Threading.Tasks;
using Warden.Common.Types;

namespace Warden.Services.Storage.ServiceClients
{
    public interface IOperationServiceClient
    {
        Task<Maybe<dynamic>> GetAsync(Guid requestId);
        Task<Maybe<T>> GetAsync<T>(Guid requestId) where T : class;
    }
}