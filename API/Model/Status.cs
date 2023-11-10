using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API.Model
{
    public partial class Status
    {
        public Status()
        {
            Libraries = new HashSet<Library>();
            Products = new HashSet<Product>();
            Supports = new HashSet<Support>();
            Users = new HashSet<User>();
        }

        public int StatusId { get; set; }
        public string? StatusName { get; set; }
        [JsonIgnore]
        public virtual ICollection<Library> Libraries { get; set; }
        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
        [JsonIgnore]
        public virtual ICollection<Support> Supports { get; set; }
        [JsonIgnore]
        public virtual ICollection<User> Users { get; set; }
    }
}
