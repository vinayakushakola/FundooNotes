using CommonLayer.RequestModels;
using CommonLayer.ResponseModels;
using System.Collections.Generic;

namespace RepositoryLayer.Interface
{
    public interface IUserRepository
    {
        ResponseData CreateAccount(SignUpRequest userSignUp);

        ResponseData UserLogin(LoginRequest login);

        List<ResponseData> GetUsersData();
    }
}
