using ECommerce.ApplicationFacade;
using ECommerce.Models;
using ECommerce.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductFacade _productFacade;

        public ProductController(IProductFacade productFacade)
        {
            _productFacade = productFacade;
        }

        [HttpGet("GetAllItems")]
        public JsonResponse<List<Item>> GetAllItems()
        {
            return (_productFacade.GetAllItems());
        }
    }
}
