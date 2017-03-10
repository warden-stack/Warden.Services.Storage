using System;
using Warden.Common.Queries;

namespace Warden.Services.Storage.ServiceClients.Queries
{
    public class GetUserSession : IQuery
    {
        public Guid Id { get; set; }
    }
}