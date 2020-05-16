using CommonLayer.RequestModels;
using CommonLayer.ResponseModels;
using System.Collections.Generic;

namespace BusinessLayer.Interface
{
    public interface IUserNoteBusiness 
    {
        UserNoteResponseData CreateNote(int userID, UserNoteRequest userNotes);

        UserNoteResponseData UpdateNote(int userID, int noteID, UpdateNoteRequest updateNoteRequest);

        bool AddReminder(int userID, int noteID, ReminderRequest reminder);

        bool AddColor(int userID, int noteID, ColorRequest color);

        bool AddImage(int userID, int noteID, ImageRequest image);

        bool DeleteNote(int noteID);

        bool TrashNote(int userID, int noteID);

        bool ArchievedUnarchievedNote(int userID, int noteID);

        bool PinUnpinNote(int userID, int noteID);

        List<UserNoteResponseData> GetAllUserNotes(int userID);

        List<UserNoteResponseData> GetTrashedNotes(int userID);

        List<UserNoteResponseData> GetArchievedNotes(int userID);

        List<UserNoteResponseData> GetPinnedNotes(int userID);

        List<UserNoteResponseData> GetReminders(int userID);

        UserNoteResponseData AddlabelsToNote(int userID, int noteID, AddLabelNoteRequest addLabelNote);

    }
}
