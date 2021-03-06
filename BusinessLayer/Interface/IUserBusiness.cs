﻿using CommonLayer.RequestModels;
using CommonLayer.ResponseModels;
using System.Collections.Generic;

namespace BusinessLayer.Interface
{
    public interface IUserBusiness
    {
        List<ResponseData> GetUsersData();

        ResponseData CreateAccount(SignUpRequest userSignUp);

        ResponseData UserLogin(LoginRequest login);

        ResponseData AddProfilePic(int userID, ProfilePicRequest profilePic);

        ResponseData ForgotPassword(ForgotPasswordRequest forgotPassword);

        ResponseData ResetPassword(int id, ResetPasswordRequest newPassword);
    }
}