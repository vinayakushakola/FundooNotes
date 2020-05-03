using CommonLayer.Models;
using CommonLayer.RequestModels;
using CommonLayer.ResponseModels;
using RepositoryLayer.ApplicationDbContext;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;

namespace RepositoryLayer.Service
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<ResponseData> GetUsersData()
        {
            try
            {
                var usersList = new List<ResponseData>();
                ResponseData responseData = null;
                var users = _context.Users;
                foreach (UserInfo user in users)
                {
                    responseData = new ResponseData()
                    {
                        ID = user.ID,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email
                    };
                    usersList.Add(responseData);
                }
                return usersList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResponseData CreateAccount(SignUpRequest userSignUp)
        {
            try
            {
                string encryptedPassword = EncryptionDecryption.Encryption(userSignUp.Password);

                UserInfo user = new UserInfo()
                {
                    FirstName = userSignUp.FirstName,
                    LastName = userSignUp.LastName,
                    Email = userSignUp.Email,
                    Password = encryptedPassword,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };
                _context.Users.Add(user);
                _context.SaveChanges();

                ResponseData responseData = new ResponseData()
                {
                    ID = user.ID,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                };
                return responseData;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResponseData UserLogin(LoginRequest login)
        {
            try
            {
                ResponseData responseData = null;
                var users = _context.Users;
                string encryptedPassword = EncryptionDecryption.Encryption(login.Password);

                foreach (UserInfo user in users)
                {
                    if (user.Email == login.Email && user.Password == encryptedPassword)
                    {
                        responseData = new ResponseData()
                        {
                            ID = user.ID,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Email = user.Email
                        };
                    }
                }
                return responseData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
