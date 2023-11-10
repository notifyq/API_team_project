using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API.Model
{
    public partial class ProductGenre
    {
        public int ProductGenreId { get; set; }
        public int? ProductId { get; set; }
        public int? GenreId { get; set; }
        [JsonIgnore]
        public virtual Genre? Genre { get; set; }
        [JsonIgnore]
        public virtual Product? Product { get; set; }
    }
}
