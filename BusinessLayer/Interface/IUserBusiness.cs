using CommonLayer.RequestModels;
using CommonLayer.ResponseModels;

namespace BusinessLayer.Interface
{
    public interface IUserBusiness
    {
        ResponseData CreateAccount(SignUpRequest userSignUp);

        ResponseData UserLogin(LoginRequest login);
    }
}
