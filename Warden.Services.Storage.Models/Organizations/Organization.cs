using System;
using System.Collections.Generic;

namespace Warden.Services.Storage.Models.Organizations
{
    public class Organization
    {
        public Guid Id { get; set; }
        public UserInOrganization Owner { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<UserInOrganization> Users { get; set; }
        public IList<Warden> Wardens { get; set; }
    }
}