﻿using System;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Common.ServiceClients.Queries;
using Warden.Services.Storage.Models.Users;

namespace Warden.Services.Storage.Providers
{
    public interface IUserProvider
    {
        Task<Maybe<AvailableResource>> IsAvailableAsync(string name);
        Task<Maybe<PagedResult<User>>> BrowseAsync(BrowseUsers query);
        Task<Maybe<User>> GetAsync(string userId);
        Task<Maybe<User>> GetByNameAsync(string name);
        Task<Maybe<UserSession>> GetSessionAsync(Guid id);
    }
}