using CommonLayer.RequestModels;
using CommonLayer.ResponseModels;
using System.Collections.Generic;

namespace RepositoryLayer.Interface
{
    public interface IUserNoteRepository
    {
        UserNoteResponseData CreateNote(int userID, UserNoteRequest userNotes);

        UserNoteResponseData UpdateNote(int userID, int noteID, UpdateNoteRequest updateNoteRequest);

        UserNoteResponseData UpdateReminder(int userID, int noteID, ReminderRequest reminder);

        bool DeleteNote(int noteID);

        bool TrashNote(int userID, int noteID);

        bool ArchievedUnarchievedNote(int userID, int noteID);

        bool PinUnpinNote(int userID, int noteID);

        List<UserNoteResponseData> GetAllUserNotes(int userID);

        List<UserNoteResponseData> GetTrashedNotes(int userID);
        
        List<UserNoteResponseData> GetArchievedNotes(int userID);

        List<UserNoteResponseData> GetPinnedNotes(int userID);

    }
}
