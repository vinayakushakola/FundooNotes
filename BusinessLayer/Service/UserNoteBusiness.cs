using BusinessLayer.Interface;
using CommonLayer.RequestModels;
using CommonLayer.ResponseModels;
using RepositoryLayer.Interface;

namespace BusinessLayer.Service
{
    public class UserNoteBusiness : IUserNoteBusiness
    {
        private readonly IUserNoteRepository _userNoteRepository;

        public UserNoteBusiness(IUserNoteRepository userNoteRepository)
        {
            _userNoteRepository = userNoteRepository;
        }
        public UserNoteResponseData CreateNote(int userID, UserNoteRequest userNotes)
        {
            UserNoteResponseData userNoteResponseData = _userNoteRepository.CreateNote(userID, userNotes);
            return userNoteResponseData;
        }
    }
}
