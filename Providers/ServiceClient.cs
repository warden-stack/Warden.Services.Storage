﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Warden.Common.Types;
using Warden.Common.Extensions;
using Warden.Services.Storage.Mappers;

namespace Warden.Services.Storage.Providers
{
    public class ServiceClient : IServiceClient
    {
        private readonly IHttpClient _httpClient;
        private readonly IMapperResolver _mapperResolver;

        public ServiceClient(IHttpClient httpClient, IMapperResolver mapperResolver)
        {
            _httpClient = httpClient;
            _mapperResolver = mapperResolver;
        }

        public async Task<Maybe<T>> GetAsync<T>(string url, string endpoint) where T : class
        {
            var data = await GetDataAsync(url, endpoint);
            if (data.HasNoValue)
                return new Maybe<T>();

            var mapper = _mapperResolver.Resolve<T>();
            var result = mapper.Map(data.Value);

            return result;
        }

        public async Task<Maybe<PagedResult<T>>> GetCollectionAsync<T>(string url, string endpoint) where T : class
        {
            var data = await GetDataAsync(url, endpoint);
            if (data.HasNoValue)
                return new Maybe<PagedResult<T>>();

            var mapper = _mapperResolver.ResolveForCollection<T>();
            var json = JsonConvert.SerializeObject(data.Value);
            var obj = JsonConvert.DeserializeObject<IEnumerable<object>>(json);
            IEnumerable<T> result = mapper.Map(obj);

            return result.PaginateWithoutLimit();
        }

        private async Task<Maybe<dynamic>> GetDataAsync(string url, string endpoint)
        {
            var response = await _httpClient.GetAsync(url, endpoint);
            if (response.HasNoValue)
                return new Maybe<dynamic>();

            var content = await response.Value.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<dynamic>(content);

            return data;
        }
    }
}