using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API.Model
{
    public partial class Content
    {
        public int ContentsId { get; set; }
        public string? ContentMessege { get; set; }
        public DateTime? Time { get; set; }
        public int? UserId { get; set; }
        public int? SupportRequestId { get; set; }
        [JsonIgnore]
        public virtual Support? SupportRequest { get; set; }
        [JsonIgnore]
        public virtual User? User { get; set; }
    }
}
