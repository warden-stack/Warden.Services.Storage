using System;

namespace Warden.Services.Storage.Models.Users
{
    public class ApiKey
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
    }
}