using Pos.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Text;


namespace Pos.DataAccess.Repositories
{
    public class ProductsRepository
    {
        public static object getProductsList()
        {
            var product = new Product { ProductId = 3 };

            var products = new List<Product>
            {
                new Product { ProductId = 1, Barcode = "123456789123", CatalogCode = "123", CatalogName = "Name1", Price = 20},
                new Product { ProductId = 2, Barcode = "", CatalogCode = "456", CatalogName = "Name2", Price = 20},
                new Product { ProductId = 3, Barcode = "789456123789", CatalogCode = "789", CatalogName = "Name3", Price = 25}
            };

            //this.ViewBag.Muie = "PSD";
            //this.ViewData["RightPrice"] = 2000.00m;
            //this.TempData

            return products;
        }
    }
}
