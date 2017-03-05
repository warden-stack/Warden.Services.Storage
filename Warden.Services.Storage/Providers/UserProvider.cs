﻿using System;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Common.ServiceClients.Queries;
using Warden.Services.Storage.Repositories;
using Warden.Common.ServiceClients;
using Warden.Services.Storage.Models.Users;

namespace Warden.Services.Storage.Providers
{
    public class UserProvider : IUserProvider
    {
        private readonly IProviderClient _providerClient;
        private readonly IUserRepository _userRepository;
        private readonly IUserSessionRepository _userSessionRepository;
        private readonly IUserServiceClient _userServiceClient;

        public UserProvider(IProviderClient providerClient,
            IUserRepository userRepository,
            IUserSessionRepository userSessionRepository,
            IUserServiceClient userServiceClient)
        {
            _providerClient = providerClient;
            _userRepository = userRepository;
            _userSessionRepository = userSessionRepository;
            _userServiceClient = userServiceClient;
        }

        public async Task<Maybe<AvailableResource>> IsAvailableAsync(string name) 
            => await _providerClient.GetAsync(
                async () => await _userRepository.IsNameAvailableAsync(name),
                async () => await _userServiceClient.IsAvailableAsync(name));

        public async Task<Maybe<PagedResult<User>>> BrowseAsync(BrowseUsers query) 
            => await _providerClient.GetCollectionAsync(
                async () => await _userRepository.BrowseAsync(query));

        public async Task<Maybe<User>> GetAsync(string userId) 
            => await _providerClient.GetAsync(
                async () => await _userRepository.GetByIdAsync(userId),
                async () => await _userServiceClient.GetAsync(userId));

        public async Task<Maybe<User>> GetByNameAsync(string name)
            => await _providerClient.GetAsync(
                async () => await _userRepository.GetByNameAsync(name),
                async () => await _userServiceClient.GetByNameAsync(name));

        public async Task<Maybe<UserSession>> GetSessionAsync(Guid id)
            => await _providerClient.GetAsync(
                async () => await _userSessionRepository.GetAsync(id),
                async () => await _userServiceClient.GetSessionAsync(id));
    }
}