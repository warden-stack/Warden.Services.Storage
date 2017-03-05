using Warden.Services.Storage.Models.Organizations;
using Warden.Services.Storage.Providers;
using Warden.Common.ServiceClients.Queries;

namespace Warden.Services.Storage.Modules
{
    public class OrganizationsModule : ModuleBase
    {
        public OrganizationsModule(IOrganizationProvider organizationProvider) : base("organizations")
        {
            Get("{id}", async args => await Fetch<GetOrganization, Organization>
                (async x => await organizationProvider.GetAsync(x.UserId, x.Id))
                .HandleAsync());
        }        
    }
}