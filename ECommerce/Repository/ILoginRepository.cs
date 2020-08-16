using ECommerce.Models;
using ECommerce.Shared;
using System.Collections.Generic;

namespace ECommerce.Repository
{
    public interface ILoginRepository
    {
        JsonResponse<string> Login(User user);
        JsonResponse< List<User>> GetAllEmployees();
        JsonResponse<User> UserLogin(User user);
        JsonResponse<int> UserSignUp(User user);
        JsonResponse<User> GetUserByUserID(string userID);
    }
}
