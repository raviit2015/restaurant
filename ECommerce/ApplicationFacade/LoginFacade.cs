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

        public JsonResponse<User> UserLogin(User user)
        {
            return _loginRepository.UserLogin(user);
        }

        public JsonResponse<int> UserSignUp(User user)
        {
            return _loginRepository.UserSignUp(user);
        }


        JsonResponse<User> ILoginFacade.GetUserByUserID(string userID)
        {
            return _loginRepository.GetUserByUserID(userID);
        }

        
    }
}
