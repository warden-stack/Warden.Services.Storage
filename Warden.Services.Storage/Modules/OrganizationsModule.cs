using Warden.Services.Organizations.Shared.Dto;
using Warden.Services.Storage.Providers;
using Warden.Services.Storage.Queries;

namespace Warden.Services.Storage.Modules
{
    public class OrganizationsModule : ModuleBase
    {
        public OrganizationsModule(IOrganizationProvider organizationProvider) : base("organizations")
        {
            Get("{id}", async args => await Fetch<GetOrganization, OrganizationDto>
                (async x => await organizationProvider.GetAsync(x.UserId, x.Id))
                .HandleAsync());
        }        
    }
}