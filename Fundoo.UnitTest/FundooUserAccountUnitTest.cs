using BusinessLayer.Interface;
using BusinessLayer.Service;
using CommonLayer.RequestModels;
using Fundoo.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.ApplicationDbContext;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using Xunit;

namespace Fundoo.UnitTest
{
    public class FundooUserAccountUnitTest
    {
        private readonly IUserBusiness _userBusiness;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IRedisCacheClient _redis;


        public static DbContextOptions<AppDbContext> AppDbContext { get; }

        public static string sqlConnectionString = "Data Source=.; Initial Catalog=FundooNotesDB; Integrated Security=true";


        static FundooUserAccountUnitTest()
        {
            AppDbContext = new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(sqlConnectionString).Options;
        }

        public FundooUserAccountUnitTest()
        {
            var context = new AppDbContext(AppDbContext);
            _userRepository = new UserRepository(context);
            _userBusiness = new UserBusiness(_userRepository);

            IConfigurationBuilder configuration = new ConfigurationBuilder();

            configuration.AddJsonFile("appsettings.json");
            _configuration = configuration.Build();
        }

        [Fact]
        public void SignUpUser_Return_OkResult()
        {
            var controller = new UserController(_userBusiness, _configuration, _redis);
            var newUserData = new SignUpRequest
            {
                FirstName = "Abcd",
                LastName = "abcdef",
                Email = "abcd1234@gmail.com",
                Password = "Abcd1234",
            };

            var data = controller.CreateAccount(newUserData);

            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public void SignUpUser_NoData_Return_BadRequest()
        {
            var controller = new UserController(_userBusiness, _configuration, _redis);
            SignUpRequest newUserData = null;

            var data = controller.CreateAccount(newUserData);

            Assert.IsType<BadRequestObjectResult>(data);
        }


        [Fact]
        public void UserLogin_ValidLoginData_Return_OkResult()
        {
            var controller = new UserController(_userBusiness, _configuration, _redis);
            var Logindata = new LoginRequest
            {
                Email = "abcd@gmail.com",
                Password = "abcd1234"
            };

            var data = controller.UserLogin(Logindata);

            Assert.IsType<OkObjectResult>(data);

        }

        [Fact]
        public void UserLogin_InvalidLoginData_Return_NotFoundResult()
        {
            var controller = new UserController(_userBusiness, _configuration, _redis);
            var Logindata = new LoginRequest
            {
                Email = "JohnCena@gmail.com",
                Password = "123456789"
            };

            var data = controller.UserLogin(Logindata);

            Assert.IsType<NotFoundObjectResult>(data);
        }


        [Fact]
        public void ForgotPassword_ValidEmailData_Return_OkResult()
        {
            var controller = new UserController(_userBusiness, _configuration, _redis);
            var email = new ForgotPasswordRequest
            {
                Email = "SamKhan2@gmail.com"
            };

            var data = controller.ForgotPassword(email);

            Assert.IsType<OkObjectResult>(data);

        }

        [Fact]
        public void ForgotPassword_NoData_Return_BadRequest()
        {
            var controller = new UserController(_userBusiness, _configuration, _redis);
            ForgotPasswordRequest forgotPassword = null;

            var data = controller.ForgotPassword(forgotPassword);

            Assert.IsType<BadRequestObjectResult>(data);

        }

        [Fact]
        public void ResetPassword_NoData_Return_BadRequest()
        {
            var controller = new UserController(_userBusiness, _configuration, _redis);
            ResetPasswordRequest resetPassword = null;

            var data = controller.ResetPassword(resetPassword);

            Assert.IsType<BadRequestObjectResult>(data);
        }

        [Fact]
        public void GetAllUsersData_ReturnOkResult()
        {
            var userController = new UserController(_userBusiness, _configuration, _redis);

            //Act
            var OkResult = userController.GetUsersData();

            //Assert
            Assert.IsType<OkObjectResult>(OkResult);
        }
    }
}
