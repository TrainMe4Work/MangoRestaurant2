using MangoWeb.Models;
using MangoWeb.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MangoWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductServices _productServices;

        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }
        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDto> productList = new();
            var response = await _productServices.GetAllProductAsync<ResponseDto>();

            if (response != null && response.IsSuccess)
            {
                productList = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            return View(productList);
        }
        public async  Task<IActionResult>ProductCreate()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductCreate(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                var response = await _productServices.CreateProductAsync<ResponseDto>(productDto);

                if (response != null && response.IsSuccess)
                {

                    return RedirectToAction(nameof(ProductIndex));
                }

            }
            
            return View(productDto);
        }

        public async Task<IActionResult> ProductEdit(int productId)
        {
            var response = await _productServices.GetProductByIdAsync<ResponseDto>(productId);

            if (response != null && response.IsSuccess)
            {
              var  productList = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(productList);
            }
            return NotFound();
           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductEdit(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                var response = await _productServices.UpdateProductAsync<ResponseDto>(productDto);

                if (response != null && response.IsSuccess)
                {

                    return RedirectToAction(nameof(ProductIndex));
                }

            }

            return View(productDto);
        }

        public async Task<IActionResult> ProductDelete(int productId)
        {
         
            var response = await _productServices.GetProductByIdAsync<ResponseDto>(productId);
            if (response != null && response.IsSuccess)
            {
                ProductDto model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]        
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductDelete(ProductDto model)
        {
            if (ModelState.IsValid)
            {
              
                var response = await _productServices.DeleteProductAsync<ResponseDto>(model.ProductId);

                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View(model);
        }
    }
}

