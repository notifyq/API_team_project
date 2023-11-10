using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API.Model
{
    public partial class Developer
    {
        public Developer()
        {
            Products = new HashSet<Product>();
        }

        public int DeveloperId { get; set; }
        public string? DeveloperName { get; set; }
        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
    }
}
