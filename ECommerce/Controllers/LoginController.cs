
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
        private IConfiguration _config;

        public LoginController(ILoginFacade loginFacade, IConfiguration config)
        {
            _loginFacade = loginFacade;
            _config = config;
        }
        [HttpGet("gettoken")]
        public Object GetToken()
        {
            string key = "my_secret_key_12345"; //Secret key which will be used later during validation    
            var issuer = "http://mysite.com";  //normally this will be your site URL    

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Create a List of Claims, Keep claims name short    
            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim("valid", "1"));
            permClaims.Add(new Claim("userid", "1"));
            permClaims.Add(new Claim("name", "bilal"));

            //Create Security Token object by giving required parameters    
            var token = new JwtSecurityToken(issuer, //Issure    
                            issuer,  //Audience    
                            permClaims,
                            expires: DateTime.Now.AddDays(1),
                            signingCredentials: credentials);
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
            return new { data = jwt_token };
        }


        [HttpGet("Test")]
        public JsonResponse<string> Test(User user)
        {
            return (_loginFacade.Login(user));
        }

        [HttpGet("GetAllEmployees")]
        public JsonResponse<List<User>> GetAllEmployees()
        {
            return (_loginFacade.GetAllEmployees());
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
