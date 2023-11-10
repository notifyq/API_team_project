using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API.Model
{
    public partial class ProductImage
    {
        public int ProductImageId { get; set; }
        public int? ProductId { get; set; }
        public string? ProductImagePath { get; set; }
        [JsonIgnore]
        public virtual Product? Product { get; set; }
    }
}
