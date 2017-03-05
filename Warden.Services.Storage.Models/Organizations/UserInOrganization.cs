using System;

namespace Warden.Services.Storage.Models.Organizations
{
    public class UserInOrganization
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}