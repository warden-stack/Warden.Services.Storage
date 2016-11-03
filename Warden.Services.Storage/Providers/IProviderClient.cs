﻿using System;
using System.Threading.Tasks;
using Warden.Common.Types;

namespace Warden.Services.Storage.Providers
{
    public interface IProviderClient
    {
        Task<Maybe<T>> GetAsync<T>(string url, string endpoint) where T : class;

        Task<Maybe<T>> GetUsingStorageAsync<T>(string url, string endpoint,
            Func<Task<Maybe<T>>> storageFetch, Func<T, Task> storageSave) where T : class;

        Task<Maybe<PagedResult<T>>> GetCollectionAsync<T>(string url, string endpoint) where T : class;

        Task<Maybe<PagedResult<T>>> GetCollectionUsingStorageAsync<T>(string url, string endpoint,
            Func<Task<Maybe<PagedResult<T>>>> storageFetch, Func<PagedResult<T>, Task> storageSave) where T : class;
    }
}