using Pos.DataAccess.Model;

namespace Pos.DataAccess.Repositories
{
    public interface IProductRepository
    {
        Product GetProduct(string code);
    }
}using Pos.DataAccess.Model;

namespace Pos.Web.Controllers
{
    public interface IProductRepository
    {
        Product GetProductByBarcode(string barcode);
    }
}