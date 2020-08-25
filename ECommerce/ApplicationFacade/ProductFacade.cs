using ECommerce.Models;
using ECommerce.Repository;
using ECommerce.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.ApplicationFacade
{
    public class ProductFacade:IProductFacade
    {
        private IProductRepository _productRepository;

        public ProductFacade(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public JsonResponse<List<Item>> GetAllItems()
        {
            return _productRepository.GetAllItems();
        }

        public JsonResponse<List<Item>> GetItemCategories()
        {
            return _productRepository.GetItemCategories();
        }

        
    }
}
