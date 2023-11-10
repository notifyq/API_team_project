using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API.Model
{
    public partial class SupportType
    {
        public SupportType()
        {
            Supports = new HashSet<Support>();
        }

        public int SupportTypeId { get; set; }
        public string? SupportTypeName { get; set; }
        [JsonIgnore]
        public virtual ICollection<Support> Supports { get; set; }
    }
}
