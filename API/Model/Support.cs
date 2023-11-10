using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API.Model
{
    public partial class Support
    {
        public Support()
        {
            Contents = new HashSet<Content>();
        }

        public int SupportId { get; set; }
        public int? UserId { get; set; }
        public int? SupportType { get; set; }
        public int? SupportStatus { get; set; }
        [JsonIgnore]
        public virtual Status? SupportStatusNavigation { get; set; }
        [JsonIgnore]
        public virtual SupportType? SupportTypeNavigation { get; set; }
        [JsonIgnore]
        public virtual User? User { get; set; }
        [JsonIgnore]
        public virtual ICollection<Content> Contents { get; set; }
    }
}
