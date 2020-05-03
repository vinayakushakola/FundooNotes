using CommonLayer.RequestModels;
using CommonLayer.ResponseModels;
using System.Collections.Generic;

namespace BusinessLayer.Interface
{
    public interface IUserBusiness
    {
        ResponseData CreateAccount(SignUpRequest userSignUp);

        ResponseData UserLogin(LoginRequest login);

        List<ResponseData> GetUsersData();

        ResponseData ForgotPassword(ForgotPasswordRequest forgotPassword);
    }
}