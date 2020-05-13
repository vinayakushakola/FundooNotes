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

        public UserNoteResponseData UpdateNote(int userID, int noteID, UpdateNoteRequest updateNoteRequest)
        {
            UserNoteResponseData userNoteResponseData = _userNoteRepository.UpdateNote(userID, noteID, updateNoteRequest);
            return userNoteResponseData;
        }

        public UserNoteResponseData UpdateReminder(int userID, int noteID, ReminderRequest reminder)
        {
            UserNoteResponseData responseData = _userNoteRepository.UpdateReminder(userID, noteID, reminder);
            return responseData;
        }

        public UserNoteResponseData AddColor(int userID, int noteID, ColorRequest color)
        {
            UserNoteResponseData responseData = _userNoteRepository.AddColor(userID, noteID, color);
            return responseData;
        }

        public bool DeleteNote(int noteID)
        {
            bool responseData = _userNoteRepository.DeleteNote(noteID);
            return responseData;
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



        public List<UserNoteResponseData> GetPinnedNotes(int userID)
        {
            List<UserNoteResponseData> userNoteResponseData = _userNoteRepository.GetPinnedNotes(userID);
            return userNoteResponseData;
        }

        public bool TrashNote(int userID, int noteID)
        {
            bool responseData = _userNoteRepository.TrashNote(userID, noteID);
            return responseData;
        }

        public bool ArchievedUnarchievedNote(int userID, int noteID)
        {
            bool responseData = _userNoteRepository.ArchievedUnarchievedNote(userID, noteID);
            return responseData;
        }

        public bool PinUnpinNote(int userID, int noteID)
        {
            bool responseData = _userNoteRepository.PinUnpinNote(userID, noteID);
            return responseData;
        }

        public bool AddImage(int userID, int noteID, ImageRequest image)
        {
            bool data = _userNoteRepository.AddImage(userID, noteID, image);
            return data;
        }

        
    }
}
