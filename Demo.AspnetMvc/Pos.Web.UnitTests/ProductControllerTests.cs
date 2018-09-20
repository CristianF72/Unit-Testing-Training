using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pos.DataAccess.Model;
using Pos.DataAccess.Repositories;
using Pos.Web.Controllers;
using Pos.Web.Models;

namespace Pos.Web.UnitTests
{
    [TestClass]
    public class ProductControllerTests 
    {
        [TestMethod]
        public void Details_ProductExists_ProductCodeIsDisplayed()
        {
            string barcode = "some product barcode";
            Product productTestData = new Product {CatalogCode = "some code"};

            IProductRepository repository = new ProductRepositoryDouble(productTestData);
            ProductController target = new ProductController(repository);


            var actionResult = target.Details(barcode);

            ProductViewModel vm = actionResult.GetViewModel<ProductViewModel>();
            Assert.AreEqual("some code", vm.Code);
        }

        [TestMethod]
        public void Details_ProductExists_PriceHasCurrencySymbol()
        {
            string barcode = "some product barcode";
            string currency = "$";
            Product productTestData = new Product { CatalogCode = "some code" };

            IProductRepository repository = new ProductRepositoryDouble(productTestData);
            ProductController target = new ProductController(repository);

            var actionResult = target.Details(barcode);

            ProductViewModel vm = actionResult.GetViewModel<ProductViewModel>();
            Assert.IsTrue(vm.Price.Contains(currency));
        }

        [TestMethod]
        public void Details_BarcodeWithUppercase_RepositoryIsCalledWithLowercaseBarcode()
        {
            string barcode = "  SOME PRODUCT BARCODE ";
            Product productTestData = new Product { CatalogCode = "some code" };
            Mock<IProductRepository> _repository = new Mock<IProductRepository>();
            _repository.Setup(r => r.GetProduct(barcode.Trim().ToLower())).Returns(productTestData);
            ProductController target = new ProductController(_repository.Object);

            var actionResult = target.Details(barcode);

            _repository.Verify(r => r.GetProduct(barcode.Trim().ToLower()));
        }
    }

    class ProductRepositoryDouble : IProductRepository
    {
        private readonly Product product;

        public ProductRepositoryDouble(Product product)
        {
            this.product = product;
        }

        public Product GetProduct(string code)
        {
            return product;
        }
    }
}
