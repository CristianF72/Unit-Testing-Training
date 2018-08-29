namespace Pos.Web.Models
{
    public class ProductViewModel
    {
        public string Id { get; set; }

        public string Barcode { get; set; }

        public string CatalogCode { get; set; }
        public string CatalogName { get; set; }
        public decimal Price { get; set; }
    }
}