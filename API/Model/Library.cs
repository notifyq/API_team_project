using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API.Model
{
    public partial class Library
    {
        public int LibraryId { get; set; }
        public int? ProductId { get; set; }
        public int? UserId { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public int? LibraryStatus { get; set; }
        [JsonIgnore]
        public virtual Status? LibraryStatusNavigation { get; set; }
        
        public virtual Product? Product { get; set; }
        [JsonIgnore]
        public virtual User? User { get; set; }
    }
}
