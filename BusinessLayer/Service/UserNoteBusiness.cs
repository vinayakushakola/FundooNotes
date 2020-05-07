using BusinessLayer.Interface;
using CommonLayer.RequestModels;
using CommonLayer.ResponseModels;
using RepositoryLayer.Interface;
using System.Collections.Generic;

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

        public string DeleteNote(int noteID)
        {
            string userNoteResponseData = _userNoteRepository.DeleteNote(noteID);
            return userNoteResponseData;
        }

        public List<UserNoteResponseData> GetTrashedNotes(int userID)
        {
            List<UserNoteResponseData> userTrashedData = _userNoteRepository.GetTrashedNotes(userID);
            return userTrashedData;
        }

        public List<UserNoteResponseData> GetAllUserNotes(int userID)
        {
            List<UserNoteResponseData> userNoteResponseData = _userNoteRepository.GetAllUserNotes(userID);
            return userNoteResponseData;
        }


    }
}
