using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        public void DeleteProduct(int ProductId) => ProductDAO.Instance.Remove(ProductId);
        public IEnumerable<Product> GetProduct() => ProductDAO.Instance.GetProductList();
        public Product GetProductById(int ProductId) => ProductDAO.Instance.GetProductByID(ProductId);
        public void InsertProduct(Product product) => ProductDAO.Instance.AddNew(product);
        public void UpdateProduct(Product product) => ProductDAO.Instance.Update(product);
    }

}
