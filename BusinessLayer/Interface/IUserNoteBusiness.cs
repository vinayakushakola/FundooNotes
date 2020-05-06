using CommonLayer.RequestModels;
using CommonLayer.ResponseModels;

namespace BusinessLayer.Interface
{
    public interface IUserNoteBusiness
    {
        UserNoteResponseData CreateNote(int userID, UserNoteRequest userNotes);
    }
}
