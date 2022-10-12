using GRPC.Server.Models;
using Grpc.Core;

namespace GRPC.Server.Services
{
    public class ProductService : Product.ProductBase
    {
        private List<ProductModel> products = new List<ProductModel>()
        {
            new ProductModel { Id = 1, Name = "Product 1", Description = "Description for product", Price = 100 },
            new ProductModel { Id = 2, Name = "Product 2", Description = "Description for product", Price = 200 }
        };

        public override Task<ProductResponse> GetProduct(ProductRequest request, ServerCallContext context)
        {
            var product = products.FirstOrDefault(x => x.Id == request.Id);
            var response = new ProductResponse()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
            
            return Task.FromResult(response);
        }

        public override async Task GetProducts(ProductEmptyRequest request, IServerStreamWriter<ProductResponse> responseStream, ServerCallContext context)
        {
            foreach(var product in products)
            {
                var response = new ProductResponse()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price
                };

                await responseStream.WriteAsync(response);
            }
        }
    }
}
