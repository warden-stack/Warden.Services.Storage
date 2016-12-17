using System;
using System.Threading.Tasks;
using Warden.Common.Types;

namespace Warden.Services.Storage.Providers
{
    public class ProviderClient : IProviderClient
    {
        public async Task<Maybe<T>> GetAsync<T>(params Func<Task<Maybe<T>>>[] fetch) where T : class
        {
            foreach (var func in fetch)
            {
                var result = await func();
                if (result.HasValue)
                    return result;
            }

            return new Maybe<T>();
        }

        public async Task<Maybe<PagedResult<T>>> GetCollectionAsync<T>(params Func<Task<Maybe<PagedResult<T>>>>[] fetch) where T : class
        {
            foreach (var func in fetch)
            {
                var result = await func();
                if (result.HasValue && result.Value.IsNotEmpty)
                    return result;
            }

            return new Maybe<PagedResult<T>>();
        }
    }
}