
using ECommerce.ApplicationFacade;
using ECommerce.Models;
using ECommerce.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILoginFacade _loginFacade;

        public LoginController(ILoginFacade loginFacade, IConfiguration config)
        {
            _loginFacade = loginFacade;
        }

        [HttpPost("Login")]
        public JsonResponse<User> Login(User user)
        {
            return (_loginFacade.UserLogin(user));
        }

        [HttpGet("GetAllEmployees")]
        public JsonResponse<List<User>> GetAllEmployees()
        {
            return (_loginFacade.GetAllEmployees());
        }


        [HttpPost("UserSignUp")]
        public JsonResponse<int> UserSignUp(User user)
        {
            return (_loginFacade.UserSignUp(user));
        }

        [HttpGet("GetUserByUserID/{userid}")]
        public JsonResponse<User> GetUserByUserID(string userId)
        {
            return (_loginFacade.GetUserByUserID(userId));
        }
        

        [Authorize]
        [HttpGet("testVerified")]
        public JsonResponse<string> testVerified()
        {
            JsonResponse<string> jsonResponse = new JsonResponse<string>();
            jsonResponse.data = "verifed user!";
            jsonResponse.status = new ServiceStatus(200);
            return jsonResponse;
        }
    }
}
