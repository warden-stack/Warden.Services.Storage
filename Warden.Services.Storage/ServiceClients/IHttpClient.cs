using System.Net.Http;
using System.Threading.Tasks;
using Warden.Common.Types;

namespace Warden.Services.Storage.ServiceClients
{
    public interface IHttpClient
    {
        void SetAuthorizationHeader(string token);
        Task<Maybe<HttpResponseMessage>> GetAsync(string url, string endpoint);
    }
}