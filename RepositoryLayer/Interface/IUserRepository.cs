using CommonLayer.RequestModels;
using CommonLayer.ResponseModels;
using System.Collections.Generic;

namespace RepositoryLayer.Interface
{
    public interface IUserRepository
    {
        List<ResponseData> GetUsersData();

        ResponseData CreateAccount(SignUpRequest userSignUp);

        ResponseData UserLogin(LoginRequest login);

        ResponseData ForgotPassword(ForgotPasswordRequest forgotPassword);

        ResponseData ResetPassword(int id, ResetPasswordRequest newPassword);
    }
}
