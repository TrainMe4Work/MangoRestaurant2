using MangoWeb.Models;
using System.Threading.Tasks;

namespace MangoWeb.Services.IServices
{
    public interface IProductServices : IBaseService
    {
        Task<T> GetAllProductAsync<T>();
        Task<T> GetProductByIdAsync<T>(int productId);
        Task<T> CreateProductAsync<T>(ProductDto productDto);
        Task<T> UpdateProductAsync<T>(ProductDto productDto);
        Task<T> DeleteProductAsync<T>(int productId);
            


    }
}
