using Core.Models;
using Dashboard.Core.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProductService : IProductService
    {
        private readonly IRedisCacheService _redisCacheService;

        public ProductService(IRedisCacheService redisCacheService)
        {
            _redisCacheService = redisCacheService;
        }
        public const string CategoryDetail = "Category:{0}";
        public const string ProductDetail = "Product:{0}";

        public Product GetProductById(int Id)
        {
            //Check Redis            
            var cacheKey = string.Format(ProductDetail, Id);
            var result = _redisCacheService.Get<Product>(cacheKey);
            //-------------------------------  

            if (result != null)
            {
                return result;
            }
            else
            {
                var model = new Product()
                {
                    ID = Id,
                    Name = "Nike Ayakkabi",
                    SeriNo = "123"
                };

                _redisCacheService.Set(cacheKey, model);
                return model;
            }
        }

        public Category GetcategoryById(int Id)
        {
            //Check Redis            
            var cacheKey = string.Format(CategoryDetail, Id);
            var result = _redisCacheService.Get<Category>(cacheKey);
            //-------------------------------  

            if (result != null)
            {
                return result;
            }
            else
            {
                var model = new Category()
                {
                    ID = Id,
                    Name = "Kahve",
                    SubCategory = "Turk Kahvesi"
                };

                _redisCacheService.Set(cacheKey, model);
                return model;
            }
        }

    }
}
