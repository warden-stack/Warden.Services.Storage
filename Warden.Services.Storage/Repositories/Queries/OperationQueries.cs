﻿using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Warden.Common.Extensions;
using Warden.Common.Mongo;
using Warden.Services.Storage.Models.Operations;

namespace Warden.Services.Storage.Repositories.Queries
{
    public static class OperationQueries
    {
        public static IMongoCollection<Operation> Operations(this IMongoDatabase database)
            => database.GetCollection<Operation>();

        public static async Task<Operation> GetByRequestIdAsync(this IMongoCollection<Operation> operations,
            Guid id)
        {
            if (id.IsEmpty())
                return null;

            return await operations.AsQueryable().FirstOrDefaultAsync(x => x.RequestId == id);
        }
    }
}