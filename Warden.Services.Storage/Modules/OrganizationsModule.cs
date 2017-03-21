using Warden.Services.Storage.Models.Organizations;
using Warden.Services.Storage.Providers;
using Warden.Services.Storage.ServiceClients.Queries;

namespace Warden.Services.Storage.Modules
{
    public class OrganizationsModule : ModuleBase
    {
        public OrganizationsModule(IOrganizationProvider organizationProvider) : base("organizations")
        {
            Get("", async args => await FetchCollection<BrowseOrganizations, Organization>
                (async x => await organizationProvider.BrowseAsync(x))
                .HandleAsync());

            Get("{id}", async args => await Fetch<GetOrganization, Organization>
                (async x => await organizationProvider.GetAsync(x.UserId, x.Id))
                .HandleAsync());
        }        
    }
}