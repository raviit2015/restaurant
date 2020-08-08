using ECommerce.Models;
using ECommerce.Shared;
using System.Collections.Generic;

namespace ECommerce.Repository
{
    public interface ILoginRepository
    {
        JsonResponse<string> Login(User user);
       JsonResponse< List<User>> GetAllEmployees();
    }
}
