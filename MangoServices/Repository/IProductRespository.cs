using MangoServices.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MangoServices.Repository
{
   public interface IProductRespository
    {
        Task<IEnumerable<ProductDto>> GetProducts();
        Task<ProductDto>GetProductById(int productId);
        Task<ProductDto>CreateUpdateProduct(ProductDto productDto);
        Task<bool>DeleteProduct(int id);
    }
}
