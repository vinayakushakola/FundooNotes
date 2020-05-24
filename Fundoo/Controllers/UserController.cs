using BusinessLayer.Interface;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Models;
using CommonLayer.RequestModels;
using CommonLayer.ResponseModels;
using Experimental.System.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
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

        private readonly IRedisCacheClient _redis;

        public UserController(IUserBusiness signUpBusiness, IConfiguration config, IRedisCacheClient redis)
        {
            _userBusiness = signUpBusiness;
            _config = config;
            _redis = redis;
        }

        /// <summary>
        /// It is used to show RedisCacheData
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("CacheData")]
        public ActionResult<IEnumerable<ResponseData>> GetUsersCacheData()
        {
            try
            {
                bool success = false;
                string message;
                var userData = _redis.Db0.GetAll<ResponseData>(new string[] { "data:key" });
                var data = userData.Values.ToList();
                if (data != null)
                {
                    success = true;
                    message = "User Data fetched successsfully";
                    return Ok(new { success, message, data });
                }
                else
                {
                    message = "No Data Present!";
                    return NotFound(new { success, message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// It shows all Register users
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// It is used for Registration
        /// </summary>
        /// <param name="signUpRequest">Sign Up Data</param>
        /// <returns>It Returns response data</returns>
        [HttpPost]
        [Route("SignUp")]
        public IActionResult CreateAccount(SignUpRequest signUpRequest)
        {
            try
            {
                ResponseData data = _userBusiness.CreateAccount(signUpRequest);
                //var redisData = _redis.Db0.Add("data:key", data, DateTimeOffset.Now.AddMinutes(10));
                bool success = false;
                string message, userFullName;
                if (data == null)
                {
                    message = "EmailID already exists!";
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

        /// <summary>
        /// It is used for Login
        /// </summary>
        /// <param name="login">login Data</param>
        /// <returns>It return Response Data</returns>
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
                    return NotFound(new { success, message });
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

        /// <summary>
        /// ForgotPassword
        /// </summary>
        /// <param name="forgotPassword">Email</param>
        /// <returns>It returns response data</returns>
        [HttpPost]
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword(ForgotPasswordRequest forgotPassword)
        {
            try
            {
                ResponseData data = _userBusiness.ForgotPassword(forgotPassword);

                bool success = false;
                string message, userFullName, jsonToken;

                if (data == null)
                {
                    message = "No User Found with that Email: " + forgotPassword.Email;
                    return Ok(new { success, message });
                }
                else
                {
                    success = true;
                    jsonToken = CreateToken(data, "ForgotPassword");

                    MSMQSender.SendToMSMQ(forgotPassword.Email, jsonToken);
                    SendMail(forgotPassword, jsonToken);

                    userFullName = data.FirstName + " " + data.LastName;
                    message = "The mail has been sent to " + forgotPassword.Email + " Successfully";

                    return Ok(new { success, message, data, jsonToken });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// It is used for changing password
        /// </summary>
        /// <param name="resetPasswordRequest">Password</param>
        /// <returns>It return response data</returns>
        [Authorize]
        [HttpPost]
        [Route("Reset")]
        public ActionResult ResetPassword(ResetPasswordRequest resetPasswordRequest)
        {
            try
            {
                bool success = false;
                string message, userFullName;
                var idClaim = User.Claims.FirstOrDefault(id => id.Type.Equals("id", StringComparison.InvariantCultureIgnoreCase));
                ResponseData data = _userBusiness.ResetPassword(Convert.ToInt32(idClaim.Value), resetPasswordRequest);
                if (data == null)
                {
                    message = "No Data Found";
                    return Ok(new { success, message });
                }
                else
                {
                    success = true;
                    userFullName = data.FirstName + " " + data.LastName;
                    message = "Hello " + userFullName + ", Your Account Password Changed Successfully";
                    return Ok(new { success, message, data });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// It is used for Uploading Profile Picture
        /// </summary>
        /// <param name="profilePic">Profile Pic</param>
        /// <returns></returns>
        [HttpPut]
        [Route("ProfilePic")]
        public IActionResult AddProfilePic(ProfilePicRequest profilePic)
        {
            try
            {
                bool success = false;
                string message;
                var idClaim = User.Claims.FirstOrDefault(id => id.Type.Equals("id", StringComparison.InvariantCultureIgnoreCase));
                ResponseData data = _userBusiness.AddProfilePic(Convert.ToInt32(idClaim.Value), profilePic);
                if (data != null)
                {
                    success = true;
                    message = "Profile Picture Added Succesfully";

                    return Ok(new { success, message, data });
                }
                else
                {
                    message = "Image Not Found!";
                    return Ok(new { success, message });
                }
            }
            catch(Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// It is used for sending mail to the user
        /// </summary>
        /// <param name="forgotPassword">Email</param>
        /// <param name="jsonToken">token</param>
        private void SendMail(ForgotPasswordRequest forgotPassword, string jsonToken)
        {
            MailMessage mail = new MailMessage();
            SmtpClient smtpserver = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("vinayak.mailtesting@gmail.com");
            mail.To.Add(forgotPassword.Email);
            mail.Subject = "reset password";
            mail.Body = "hi, you requested for password reset! \n\nuse this token for password reset!\n\ntoken: " + jsonToken;

            smtpserver.Port = 587;
            smtpserver.Credentials = new System.Net.NetworkCredential("vinayak.mailtesting@gmail.com", "@bcd.1234");
            smtpserver.EnableSsl = true;

            smtpserver.Send(mail);
        }

        /// <summary>
        /// It Create Token
        /// </summary>
        /// <param name="responseData">Response Data</param>
        /// <param name="type"></param>
        /// <returns></returns>
        private string CreateToken(ResponseData responseData, string type)
        {
            try
            {
                var symmetricSecuritykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

                var signingCreds = new SigningCredentials(symmetricSecuritykey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Role, type),
                    new Claim("ID", responseData.ID.ToString()),
                    new Claim("Email", responseData.Email.ToString())
                };

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
