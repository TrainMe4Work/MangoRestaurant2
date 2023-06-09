﻿using AutoMapper;
using MangoServices.DbContexts;
using MangoServices.Models;
using MangoServices.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MangoServices.Repository
{
    public class ProductRepository : IProductRespository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;
        public ProductRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            List<Product> productList = await _db.Products.ToListAsync();
            return _mapper.Map<List<ProductDto>>(productList);
        }
        public async Task<ProductDto> GetProductById(int productId)
        {
            Product product = await _db.Products.Where(x=>x.ProductId== productId).FirstOrDefaultAsync();
            return _mapper.Map<ProductDto>(product);
        }
        public async Task<bool> DeleteProduct(int productId)
        {
            try
            {
                
                var productToDelete = await _db.Products.FirstOrDefaultAsync(u => u.ProductId == productId);
                if(productToDelete== null)
                {
                    return false;
                }
                _db.Products.Remove(productToDelete);
                await _db.SaveChangesAsync();
                return true;
                
            }
            catch (Exception)
            {

                return false;
            }
        }        

        public async Task<ProductDto> CreateUpdateProduct(ProductDto productDto)
        {
            Product product = _mapper.Map<ProductDto, Product>(productDto);
            if (product.ProductId > 0)
            {
                _db.Products.Update(product);
                await _db.SaveChangesAsync();
            }
            else
            {
                _db.Products.Add(product);
                await _db.SaveChangesAsync();

            }
            

            return _mapper.Map<Product, ProductDto>(product);
        }

        
        
        
    }
}
