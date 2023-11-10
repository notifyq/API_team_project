namespace API.Model
{
    public class ProductAdd
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public decimal? ProductPrice { get; set; }
        public int? ProductPublisher { get; set; }
        public int? ProductDeveloper { get; set; }
        public int? ProductStatus { get; set; }
    }
}
