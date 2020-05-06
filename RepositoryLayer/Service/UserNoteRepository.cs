using CommonLayer.Models;
using CommonLayer.RequestModels;
using CommonLayer.ResponseModels;
using RepositoryLayer.ApplicationDbContext;
using RepositoryLayer.Interface;
using System;

namespace RepositoryLayer.Service
{
    public class UserNoteRepository : IUserNoteRepository
    {
        private readonly AppDbContext _context;

        public UserNoteRepository(AppDbContext context)
        {
            _context = context;
        }
        public UserNoteResponseData CreateNote(int userID, UserNoteRequest userNoteData)
        {
            try
            {
                UserNotesInfo userNote = new UserNotesInfo()
                {
                    UserId = userID,
                    Title = userNoteData.Title,
                    Notes = userNoteData.Notes,
                    Color = userNoteData.Color,
                    Image = userNoteData.Image,
                    Pin = userNoteData.Pin,
                    Archived = userNoteData.Archived,
                    Trash = userNoteData.Trash,
                    Reminder = userNoteData.Reminder,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };
                _context.UserNotes.Add(userNote);
                _context.SaveChanges();

                UserNoteResponseData noteResponseData = new UserNoteResponseData()
                {
                    NoteId = userNote.NotesId,
                    Title = userNote.Title,
                    Notes = userNote.Notes,
                    Color = userNote.Color,
                    Image = userNote.Image,
                    Pin = userNote.Pin,
                    Archived = userNote.Archived,
                    Trash = userNote.Trash
                };
                return noteResponseData;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string DeleteNote(int noteID)
        {
            try
            {
                _context.UserNotes.Remove(_context.UserNotes.Find(noteID));
                _context.SaveChanges();

                var userNotes = _context.UserNotes;
                string data = "Notes Deleted Successfully";
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
