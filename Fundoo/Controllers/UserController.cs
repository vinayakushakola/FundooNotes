using BusinessLayer.Interface;
using CommonLayer.RequestModels;
using CommonLayer.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Fundoo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness _userBusiness;

        private readonly IConfiguration _config;

        public UserController(IUserBusiness signUpBusiness, IConfiguration config)
        {
            _userBusiness = signUpBusiness;
            _config = config;
        }

        [HttpGet]
        [Authorize]
        public ActionResult GetUsersData()
        {
            try
            {
                List<ResponseData> userData = _userBusiness.GetUsersData();
                return Ok(userData.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpPost]
        [Route("SignUp")]
        public IActionResult CreateAccount(SignUpRequest signUpRequest)
        {
            try
            {
                ResponseData data = _userBusiness.CreateAccount(signUpRequest);
                bool success = false;
                string message, userFullName;
                if (data == null)
                {
                    message = "Enter Valid Details!";
                    return Ok(new { success, message });
                }
                else
                {
                    success = true;
                    userFullName = data.FirstName + " " + data.LastName;
                    message = "Hello " + userFullName + ", Your Account Created Successfully";
                    return Ok(new { success, message, data });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult UserLogin(LoginRequest login)
        {
            try
            {
                ResponseData data = _userBusiness.UserLogin(login);
                bool success = false;
                string message, userFullName, jsonToken;

                if (data == null)
                {
                    message = "Enter Valid Email & Password";
                    return Ok(new { success, message });
                }
                else
                {
                    success = true;
                    userFullName = data.FirstName + " " + data.LastName;
                    message = "Hello " + userFullName + ", You Logged in Successfully";
                    jsonToken = CreateToken(data, "login");
                    return Ok(new { success, message, data, jsonToken });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        public string CreateToken(ResponseData responseData, string type)
        {
            try
            {
                // Symmetric Security key
                var symmetricSecuritykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

                // Signing credentials
                var signingCreds = new SigningCredentials(symmetricSecuritykey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Role, type));
                claims.Add(new Claim("ID", responseData.ID.ToString()));
                claims.Add(new Claim("email", responseData.Email.ToString()));


                // Create Token
                var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                    _config["Jwt:Issuer"],
                    claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: signingCreds);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}