using BusinessLayer.Interface;
using CommonLayer.RequestModels;
using CommonLayer.ResponseModels;
using RepositoryLayer.Interface;
using System.Collections.Generic;

namespace BusinessLayer.Service
{
    public class UserBusiness : IUserBusiness 
    {
        private readonly IUserRepository _userRepository;

        public UserBusiness(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<ResponseData> GetUsersData()
        {
            List<ResponseData> usersList = _userRepository.GetUsersData();
            return usersList;
        }

        public ResponseData ResetPassword(int id, ResetPasswordRequest resetPasswordRequest)
        {
            ResponseData responseData = _userRepository.ResetPassword(id, resetPasswordRequest);
            return responseData;
        }


        public ResponseData CreateAccount(SignUpRequest userSignUp)
        {
            ResponseData responseData = _userRepository.CreateAccount(userSignUp);
            return responseData;
        }

        public ResponseData UserLogin(LoginRequest login)
        {
            ResponseData responseData = _userRepository.UserLogin(login);
            return responseData;
        }


        public ResponseData AddProfilePic(int userID, ProfilePicRequest profilePic)
        {
            ResponseData responseData = _userRepository.AddProfilePic(userID, profilePic);
            return responseData;
        }

        public ResponseData ForgotPassword(ForgotPasswordRequest forgotPassword)
        {
            ResponseData responseData = _userRepository.ForgotPassword(forgotPassword);
            return responseData;
        }
    }
}
