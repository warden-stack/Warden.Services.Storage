using System;
using System.Collections.Generic;

namespace Warden.Services.Storage.Models.Organizations
{
    public class Warden
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public DateTime CreatedAt { get; set; }
        public IList<Watcher> Watchers { get; set; }
    }
}