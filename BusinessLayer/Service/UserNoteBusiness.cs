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

        public List<UserNoteResponseData> GetArchievedNotes(int userID)
        {
            List<UserNoteResponseData> userArchievedData = _userNoteRepository.GetArchievedNotes(userID);
            return userArchievedData;
        }

        public List<UserNoteResponseData> GetAllUserNotes(int userID)
        {
            List<UserNoteResponseData> userNoteResponseData = _userNoteRepository.GetAllUserNotes(userID);
            return userNoteResponseData;
        }

        public UserNoteResponseData UpdateNote(int userID, int noteID, UpdateNoteRequest updateNoteRequest)
        {
            UserNoteResponseData userNoteResponseData = _userNoteRepository.UpdateNote(userID, noteID, updateNoteRequest);
            return userNoteResponseData;
        }

        public List<UserNoteResponseData> GetPinnedNotes(int userID)
        {
            List<UserNoteResponseData> userNoteResponseData = _userNoteRepository.GetPinnedNotes(userID);
            return userNoteResponseData;
        }

    }
}
