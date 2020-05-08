using CommonLayer.RequestModels;
using CommonLayer.ResponseModels;
using System.Collections.Generic;

namespace BusinessLayer.Interface
{
    public interface IUserNoteBusiness 
    {
        UserNoteResponseData CreateNote(int userID, UserNoteRequest userNotes);

        string DeleteNote(int noteID);

        UserNoteResponseData UpdateNote(int userID, int noteID, UpdateNoteRequest updateNoteRequest);

        List<UserNoteResponseData> GetAllUserNotes(int userID);

        List<UserNoteResponseData> GetTrashedNotes(int userID);

        List<UserNoteResponseData> GetArchievedNotes(int userID);

    }
}
