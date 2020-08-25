using ECommerce.Models;
using ECommerce.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Repository
{
    public interface IProductRepository
    {
        JsonResponse<List<Item>> GetAllItems();
        JsonResponse<List<Item>> GetItemCategories();
    }
}
