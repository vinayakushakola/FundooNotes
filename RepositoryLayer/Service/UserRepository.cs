using CommonLayer.Models;
using CommonLayer.RequestModels;
using CommonLayer.ResponseModels;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.ApplicationDbContext;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

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
                List<ResponseData> userLists = _context.Users.
                    Where(user => user.ID>0).
                    Select(user => new ResponseData
                    {
                        ID = user.ID,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        ProfilePic = user.ProfilePic,
                        Email = user.Email
                    }).
                    ToList();
                return userLists;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResponseData ResetPassword(int id, ResetPasswordRequest resetPasswordRequest)
        {
            try
            {
                ResponseData responseData = null;

                var user = _context.Users.First(userID => userID.ID == id);
                string newPassword = EncryptionDecryption.Encryption(resetPasswordRequest.NewPassword);
                user.Password = newPassword;
                _context.SaveChanges();

                responseData = new ResponseData()
                {
                    ID = user.ID,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    ProfilePic = user.ProfilePic,
                    Email = user.Email
                };
                return responseData;
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

                var checkUsers = _context.Users.
                    Where(existedUser => existedUser.Email == userSignUp.Email).
                    FirstOrDefault();
                if (checkUsers == null)
                {
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
                        ProfilePic = user.ProfilePic,
                        Email = user.Email
                    };
                    return responseData;
                }
                else
                {
                    return null;
                }
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
                string encryptedPassword = EncryptionDecryption.Encryption(login.Password);
                var userData = _context.Users.
                    Where(user => user.Email == login.Email && user.Password == encryptedPassword)
                    .FirstOrDefault<UserInfo>();

                if (userData != null)
                {
                    responseData = new ResponseData()
                    {
                        ID = userData.ID,
                        FirstName = userData.FirstName,
                        LastName = userData.LastName,
                        ProfilePic = userData.ProfilePic,
                        Email = userData.Email
                    };
                }
                return responseData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResponseData AddProfilePic(int userID, ProfilePicRequest profilePic)
        {
            try
            {
                ResponseData responseData = null;
                var userData = _context.Users.
                    Where(user => user.ID == userID).
                    First<UserInfo>();
                userData.ProfilePic = profilePic.ProfilePic;
                _context.SaveChanges();
                if (userData != null)
                {
                    responseData = new ResponseData()
                    {
                        ID = userData.ID,
                        FirstName = userData.FirstName,
                        LastName = userData.LastName,
                        ProfilePic = userData.ProfilePic,
                        Email = userData.Email
                    };
                }
                return responseData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResponseData ForgotPassword(ForgotPasswordRequest forgotPassword)
        {
            try
            {
                ResponseData responseData = null;
                var userData = _context.Users.FirstOrDefault(user => user.Email == forgotPassword.Email);
                if (userData != null)
                {
                    responseData = new ResponseData()
                    {
                        ID = userData.ID,
                        FirstName = userData.FirstName,
                        LastName = userData.LastName,
                        Email = userData.Email
                    };
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
