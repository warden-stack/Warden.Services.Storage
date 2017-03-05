using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Warden.Messages.Events;
using Warden.Services.Storage.Models.Organizations;
using Warden.Messages.Events.Organizations;
using Warden.Services.Storage.Repositories;

namespace Warden.Services.Storage.Handlers
{
    public class OrganizationCreatedHandler : IEventHandler<OrganizationCreated>
    {
        private readonly IOrganizationRepository _organizationRepository;

        public OrganizationCreatedHandler(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        public async Task HandleAsync(OrganizationCreated @event)
        {
            var organization = await _organizationRepository.GetAsync(@event.UserId, @event.Name);
            if (organization.HasValue)
                return;

            var owner = new UserInOrganization
                        {
                            UserId = @event.UserId,
                            Email = @event.UserEmail,
                            Role = @event.UserOrganizationRole,
                            CreatedAt = @event.UserCreatedAt
                        };

            await _organizationRepository.AddAsync(new Organization
            {
                Id = @event.OrganizationId,
                Name = @event.Name,
                Description = @event.Description,
                Owner = owner,
                Users = new List<UserInOrganization> { owner },
                Wardens = new List<Warden.Services.Storage.Models.Organizations.Warden>()
            });
        }
    }
}