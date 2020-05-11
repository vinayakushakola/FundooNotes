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
using System;
using Xunit;

namespace Fundoo.UnitTest
{
    public class FundooUserAccountUnitTest
    {
        private readonly IUserBusiness _userBusiness;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

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

        }


        [Fact]
        public void Task_RegisterUser_Return_OkResult()
        {
            var controller = new UserController(_userBusiness, _configuration);
            var newUserData = new SignUpRequest
            {
                FirstName = "Somesh",
                LastName = "Rawat",
                Email = "Somesh1234@gmail.com",
                Password = "Somesh1234",
            };

            var data = controller.CreateAccount(newUserData);

            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public void GetAllUsersData_ReturnOkResult()
        {
            var userController = new UserController(_userBusiness, _configuration);

            //Act
            var OkResult = userController.GetUsersData();

            //Assert
            Assert.IsType<OkObjectResult>(OkResult);
        }
    }
}
