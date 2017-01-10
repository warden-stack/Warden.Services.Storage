using Warden.Common.Queries;

namespace Warden.Services.Storage.Queries
{
    public class GetNameAvailability : IQuery
    {
        public string Name { get; set; }
    }
}