using BusinessLayer.Interface;
using CommonLayer.RequestModels;
using CommonLayer.ResponseModels;
using RepositoryLayer.Interface;

namespace BusinessLayer.Service
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepository _userRepository;

        public UserBusiness(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
    }
}
