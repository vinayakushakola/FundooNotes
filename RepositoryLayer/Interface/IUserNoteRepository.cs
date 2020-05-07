using CommonLayer.RequestModels;
using CommonLayer.ResponseModels;
using System.Collections.Generic;

namespace RepositoryLayer.Interface
{
    public interface IUserNoteRepository
    {
        UserNoteResponseData CreateNote(int userID, UserNoteRequest userNotes);

        string DeleteNote(int noteID);

        List<UserNoteResponseData> GetAllUserNotes(int userID);

        List<UserNoteResponseData> GetTrashedNotes(int userID);

    }
}
