using ECommerce.Models;
using ECommerce.Repository;
using ECommerce.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ECommerce.ApplicationFacade
{
    public class LoginFacade : ILoginFacade
    {
        private ILoginRepository _loginRepository;

        public LoginFacade(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public JsonResponse<List<User>> GetAllEmployees()
        {
            return _loginRepository.GetAllEmployees();
        }

        public JsonResponse<string> Login(User user)
        {
             return _loginRepository.Login(user);
        }
    }
}
