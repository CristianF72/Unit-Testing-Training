using System.ComponentModel.DataAnnotations;

namespace Pos.DataAccess.Model
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Barcode { get; set; }
        public string CatalogCode { get; set; }
        public string CatalogName { get; set; }
        public decimal Price { get; set; }
    }
}
