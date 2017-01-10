using Warden.Common.Queries;

namespace Warden.Services.Storage.Queries
{
    public class GetUserByName : IQuery
    {
        public string Name { get; set; }
    }
}