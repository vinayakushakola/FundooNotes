using BusinessLayer.Interface;
using CommonLayer.RequestModels;
using CommonLayer.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fundoo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness _userBusiness;

        public UserController(IUserBusiness signUpBusiness)
        {
            _userBusiness = signUpBusiness;
        }

        [HttpGet]
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
                string message, userFullName;

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
                    return Ok(new { success, message, data });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}