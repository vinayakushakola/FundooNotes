using CommonLayer.RequestModels;
using CommonLayer.ResponseModels;

namespace RepositoryLayer.Interface
{
    public interface IUserNoteRepository
    {
        UserNoteResponseData CreateNote(int userID, UserNoteRequest userNotes);

        string DeleteNote(int noteID);
    }
}
