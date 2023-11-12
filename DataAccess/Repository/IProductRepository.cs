using BusinessObject;

namespace DataAccess.Repository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProduct();
        Product GetProductById(int ProductId);
        void InsertProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int ProductId);

    }
}