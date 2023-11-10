using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API.Model
{
    public partial class User
    {
        public User()
        {
            Contents = new HashSet<Content>();
            Libraries = new HashSet<Library>();
            Supports = new HashSet<Support>();
        }

        public int IdUser { get; set; }
        public string? UserLogin { get; set; }
        public string? UserPassword { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? UserImage { get; set; }
        public int? UserRole { get; set; }
        public int? UserStatus { get; set; }
        public virtual Role? UserRoleNavigation { get; set; }
        public virtual Status? UserStatusNavigation { get; set; }
        [JsonIgnore]
        public virtual ICollection<Content> Contents { get; set; }
        public virtual ICollection<Library> Libraries { get; set; }
        [JsonIgnore]
        public virtual ICollection<Support> Supports { get; set; }
    }
}
