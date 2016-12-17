using System;
using System.Threading.Tasks;
using Warden.Common.Types;

namespace Warden.Services.Storage.Providers
{
    public interface IProviderClient
    {
        Task<Maybe<T>> GetAsync<T>(params Func<Task<Maybe<T>>>[] fetch)
            where T : class;

        Task<Maybe<PagedResult<T>>> GetCollectionAsync<T>(params Func<Task<Maybe<PagedResult<T>>>>[] fetch)
            where T : class;
    }
}