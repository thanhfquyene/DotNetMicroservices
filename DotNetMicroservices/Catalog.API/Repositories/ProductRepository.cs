using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }

        public async Task CreateProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            var update = await _context.Products.DeleteOneAsync(filter);

            return update.IsAcknowledged && update.DeletedCount > 0;
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, categoryName);
            var product = await _context.Products.Find(filter).ToListAsync();

            return product;
        }

        public async Task<Product> GetProductById(string id)
        {
            var product = await _context.Products.Find(x => x.Id == id).FirstOrDefaultAsync();

            return product;
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.ElemMatch(p => p.Name, name);
            var product = await _context.Products.Find(filter).ToListAsync();

            return product;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products = await _context.Products.Find(x => true).ToListAsync();

            return products;
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var update = await _context.Products
                .ReplaceOneAsync(filter: p => p.Id == product.Id, replacement: product);

            return update.IsAcknowledged && update.ModifiedCount > 0;        }
    }
}
