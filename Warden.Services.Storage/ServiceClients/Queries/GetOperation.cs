﻿using System;
using Warden.Common.Queries;

namespace Warden.Services.Storage.ServiceClients.Queries
{
    public class GetOperation : IQuery
    {
        public Guid RequestId { get; set; }
    }
}