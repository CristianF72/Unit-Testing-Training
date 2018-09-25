using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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
        public void Details_ProductExists_ProductCodeDisplayed()
        {
            Product testDataProduct = new Product {CatalogCode = "some code"};
            ProductController target = GetTargetWithProduct(testDataProduct);

            IActionResult result = target.Details("some barcode");

            ProductViewModel vm = result.GetViewModel<ProductViewModel>();
            Assert.AreEqual("some code", vm.Code);
        }

        [TestMethod]
        public void Details_NoProductForBarcode_NotFoundInName()
        {
            ProductController target = GetTargetWithoutProduct();
            IActionResult result = target.Details("some barcode");

            ProductViewModel vm = result.GetViewModel<ProductViewModel>();
            Assert.AreEqual("Not Available", vm.Name);
        }

        [TestMethod]
        [Ignore]
        public void List_MoreProducts_OrderedByName()
        {
            //TODO: complete this test as part of exercise 11

            Mock<IRepository> repStub  = new Mock<IRepository>();
            Product[] testProducts = new[]
            {
                new Product{CatalogName = "b"},
                new Product{CatalogName = "A"}
            };
            repStub.Setup(r => r.GetEntity<Product>())
                .Returns(testProducts.AsQueryable());

            ProductController target = new ProductController(repStub.Object);

            var result = target.List();

            Assert.Fail("Not implemented");
        }


        [TestMethod]
        public void Details_ProductExists_VatAppliedAndCurrencySymbolAdded()
        {
            decimal somePrice = 11.12m;
            Product testDataProduct = new Product { Price = somePrice };
            ProductController target = GetTargetWithProduct(testDataProduct);


            IActionResult result = target.Details("some barcode");

            ProductViewModel vm = result.GetViewModel<ProductViewModel>();
            Assert.AreEqual("11.12 $", vm.Price);
        }

        private ProductController GetTargetWithoutProduct()
        {
            Mock<IProductRepository> productRepositoryStub  = new Mock<IProductRepository>();
            return GetTarget(productRepositoryStub.Object);
        }

        private static ProductController GetTargetWithProduct(Product testDataProduct)
        {
            IProductRepository repository = GetRepositoryStub(testDataProduct);
            ProductController target = GetTarget(repository);
            ;
            return target;
        }

        private static IProductRepository GetRepositoryStub(Product testDataProduct)
        {
            IProductRepository repository = new ProductRepositoryDouble(testDataProduct);
            return repository;
        }

        private static ProductController GetTarget(IProductRepository repository)
        {
            Mock<IPriceCalculator> priceCalculatorStub = new Mock<IPriceCalculator>();
            priceCalculatorStub
                .Setup(p => p.GetPrice(It.IsAny<Product>()))
                .Returns<Product>(p => p.Price);

            return new ProductController(repository, priceCalculatorStub.Object);
        }
        //[TestMethod]
        //public void Details_ProductExists_ProductCodeIsDisplayed()
        //{
        //    string barcode = "some product barcode";
        //    Product productTestData = new Product {CatalogCode = "some code"};

        //    IProductRepository repository = new ProductRepositoryDouble(productTestData);
        //    IProductRepository priceCalculator = new ProductRepositoryDouble(productTestData);
        //    ProductController target = new ProductController(repository, priceCalculator);


        //    var actionResult = target.Details(barcode);

        //    ProductViewModel vm = actionResult.GetViewModel<ProductViewModel>();
        //    Assert.AreEqual("some code", vm.Code);
        //}

        [TestMethod]
        public void Details_ProductExists_PriceHasCurrencySymbol()
        {
            string barcode = "some product barcode";
            string currency = "$";
            Product productTestData = new Product { CatalogCode = "some code" };

            IProductRepository repository = new ProductRepositoryDouble(productTestData);
            IPriceCalculator priceCalculator = new ProductRepositoryDouble(productTestData);
            ProductController target = new ProductController(repository, priceCalculator);

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
            IPriceCalculator priceCalculator = new ProductRepositoryDouble(productTestData);
            _repository.Setup(r => r.GetProduct(barcode.Trim().ToLower())).Returns(productTestData);
            ProductController target = new ProductController(_repository.Object, priceCalculator);

            var actionResult = target.Details(barcode);

            _repository.Verify(r => r.GetProduct(barcode.Trim().ToLower()));
        }
    }

    class ProductRepositoryDouble : IProductRepository, IPriceCalculator
    {
        private readonly Product product;

        public ProductRepositoryDouble(Product product)
        {
            this.product = product;
        }

        public decimal GetPrice(Product product)
        {
            throw new NotImplementedException();
        }

        public Product GetProduct(string code)
        {
            return product;
        }

        public decimal GetVta(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
