using MangoServices.Models.Dtos;
using MangoServices.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MangoServices.Controllers
{
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        public IProductRespository _iproductRepost;       
        protected ResponseDto _responseDto;

        public ProductController(IProductRespository iproductRepos)
        {
            _iproductRepost = iproductRepos;
            this._responseDto = new ResponseDto();
           
        }
        [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                IEnumerable<ProductDto> product = await _iproductRepost.GetProducts();
                _responseDto.Result = product;

            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = new List<string>() { ex.ToString() };
                
            }

            return _responseDto;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<object> Get(int id)
        {
            try
            {
                ProductDto product = await _iproductRepost.GetProductById(id);
                _responseDto.Result = product;

            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = new List<string>() { ex.ToString() };

            }

            return _responseDto;
        }

        [HttpPost]        
        public async Task<object> Post([FromBody] ProductDto productDto)
        {
            try
            {
                ProductDto product = await _iproductRepost.CreateUpdateProduct(productDto);
                _responseDto.Result = product;

            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = new List<string>() { ex.ToString() };

            }

            return _responseDto;
        }

        [HttpPut]
        public async Task<object> Put([FromBody] ProductDto productDto)
        {
            try
            {
                ProductDto product = await _iproductRepost.CreateUpdateProduct(productDto);
                _responseDto.Result = product;

            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = new List<string>() { ex.ToString() };

            }

            return _responseDto;
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<object> Delete(int id)
        {
            try
            {
                bool isSuccess = await _iproductRepost.DeleteProduct(id);
                _responseDto.Result = isSuccess;

            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = new List<string>() { ex.ToString() };

            }

            return _responseDto;
        }
    }
}
