using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API.Model
{
    public partial class Genre
    {
        public Genre()
        {
            ProductGenres = new HashSet<ProductGenre>();
        }

        public int GenreId { get; set; }
        public string? GenreName { get; set; }
        [JsonIgnore]
        public virtual ICollection<ProductGenre> ProductGenres { get; set; }
    }
}
