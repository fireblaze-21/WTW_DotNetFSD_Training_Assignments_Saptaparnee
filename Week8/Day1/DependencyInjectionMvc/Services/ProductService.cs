using DependencyInjectionMvc.Models;
using DependencyInjectionMvc.Repositories;

namespace DependencyInjectionMvc.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;

        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Product> GetProducts()
        {
            return _repo.GetAll();
        }

        public Product GetProductById(int id)
        {
            return _repo.GetById(id);
        }

        public void CreateProduct(Product product)
        {
            _repo.Add(product);
            _repo.Save();
        }

        public void UpdateProduct(Product product)
        {
            _repo.Update(product);
            _repo.Save();
        }

        public void DeleteProduct(int id)
        {
            var product = _repo.GetById(id);
            if (product != null)
            {
                _repo.Delete(product);
                _repo.Save();
            }
        }
    }
}
