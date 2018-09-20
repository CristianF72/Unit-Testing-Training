using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Pos.DataAccess.Model;
using Pos.DataAccess.Repositories;
using Pos.Web.Models;

namespace Pos.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IRepository rep;
        private readonly IProductRepository repository;
        private readonly IPriceCalculator priceCalculator;

        public ProductController(IRepository rep)
        {
            this.rep = rep;
        }

        public ProductController(IProductRepository repository, IPriceCalculator priceCalculator)
        {
            this.repository = repository;
            this.priceCalculator = priceCalculator;
        }

        private readonly IProductRepository repository;

        public ProductController(IProductRepository repository)
        {
            this.repository = repository;
        }

        public IActionResult Details(string barcode)
        {
            string code = barcode.Trim().ToLowerInvariant();
            Product p = repository.GetProduct(code);

            ProductViewModel vm;
            if (p != null)
            {
                decimal price = priceCalculator.GetPrice(p);
                vm = new ProductViewModel
                {
                    Code = p.CatalogCode,
                    Name = p.CatalogName,
                    Price = $"{price} $",
                };
            }
            else
                vm = new ProductViewModel {Name = "Not Available"};

            return View(vm);
        }

        public IActionResult List()
        {
            var list = rep.GetEntity<Product>()
                .OrderBy(p=>p.CatalogName)
                .ToList();
            return View(list);
        }

        //public JsonResult ApiDetails(string barcode)
        //{
        //    return Json(new{ Id =barcode });
        //}
    }
}