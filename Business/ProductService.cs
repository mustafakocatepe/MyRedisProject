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
            ProductList.Add(new Product { ID = 1, Name = "Nike", SeriNo = "47528" });
            ProductList.Add(new Product { ID = 2, Name = "Adidas", SeriNo = "65921" });
            ProductList.Add(new Product { ID = 3, Name = "Vans", SeriNo = "84528" });
            ProductList.Add(new Product { ID = 4, Name = "Skechers", SeriNo = "32845" });
        }
        public const string CategoryDetail = "Category:{0}";
        public const string ProductDetail = "Product:{0}";
        List<Product> ProductList = new List<Product>();
        //public const string ProductDetail = "Product:asd:{0}";

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
                var data = ProductList.Where(x => x.ID == Id).FirstOrDefault();
                _redisCacheService.Set(cacheKey, data);
                return data;
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
