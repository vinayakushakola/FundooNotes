using CommonLayer.Models;
using CommonLayer.RequestModels;
using CommonLayer.ResponseModels;
using RepositoryLayer.ApplicationDbContext;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

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


        public UserNoteResponseData UpdateNote(int userID, int noteID, UpdateNoteRequest updateNoteRequest)
        {
            try
            {
                UserNoteResponseData userNoteResponseData = null;
                var userData = _context.UserNotes.FirstOrDefault(user => user.UserId == userID && user.NotesId == noteID);
                userData.Title = updateNoteRequest.Title;
                userData.Notes = updateNoteRequest.Notes;
                _context.SaveChanges();

                userNoteResponseData = new UserNoteResponseData()
                {
                    NoteId = userData.NotesId,
                    Title = userData.Title,
                    Notes = userData.Notes,
                    Color = userData.Color,
                    Image = userData.Image,
                    Pin = userData.Pin,
                    Archived = userData.Archived,
                    Trash = userData.Trash,
                    Reminder = userData.Reminder
                };
                return userNoteResponseData;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public UserNoteResponseData UpdateReminder(int userID, int noteID, ReminderRequest reminder)
        {
            try
            {
                UserNoteResponseData userNoteResponseData = null;
                var userData = _context.UserNotes.FirstOrDefault(user => user.UserId == userID && user.NotesId == noteID);
                userData.Reminder = reminder.Reminder;
                _context.SaveChanges();

                userNoteResponseData = new UserNoteResponseData()
                {
                    NoteId = userData.NotesId,
                    Title = userData.Title,
                    Notes = userData.Notes,
                    Color = userData.Color,
                    Image = userData.Image,
                    Pin = userData.Pin,
                    Archived = userData.Archived,
                    Trash = userData.Trash
                };
                return userNoteResponseData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteNote(int noteID)
        {
            try
            {
                _context.UserNotes.Remove(_context.UserNotes.Find(noteID));
                _context.SaveChanges();

                var userNotes = _context.UserNotes;
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public List<UserNoteResponseData> GetAllUserNotes(int userID)
        {
            try
            {
                List<UserNoteResponseData> userNoteLists = _context.UserNotes.
                    Where(user => user.UserId == userID && user.Archived != true && user.Trash != true && user.Pin != true).
                    Select(user => new UserNoteResponseData
                    {
                        NoteId = user.NotesId,
                        Title = user.Title,
                        Notes = user.Notes,
                        Color = user.Color,
                        Image = user.Image,
                        Pin = user.Pin,
                        Archived = user.Archived,
                        Trash = user.Trash,
                        Reminder = user.Reminder
                    }).
                    ToList();

                if (userNoteLists == null)
                {
                    return null;
                }
                return userNoteLists;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public List<UserNoteResponseData> GetTrashedNotes(int userID)
        {
            try
            {
                List<UserNoteResponseData> userNoteLists = _context.UserNotes.
                    Where(user => user.UserId == userID && user.Trash == true).
                    Select(user => new UserNoteResponseData
                    {
                        NoteId = user.NotesId,
                        Title = user.Title,
                        Notes = user.Notes,
                        Color = user.Color,
                        Image = user.Image,
                        Pin = user.Pin,
                        Archived = user.Archived,
                        Trash = user.Trash,
                        Reminder = user.Reminder
                    }).
                    ToList();

                if (userNoteLists == null)
                {
                    return null;
                }
                return userNoteLists;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<UserNoteResponseData> GetArchievedNotes(int userID)
        {
            try
            {
                List<UserNoteResponseData> userNoteLists = _context.UserNotes.
                    Where(user => user.UserId == userID && user.Archived == true).
                    Select(user => new UserNoteResponseData
                    {
                        NoteId = user.NotesId,
                        Title = user.Title,
                        Notes = user.Notes,
                        Color = user.Color,
                        Image = user.Image,
                        Pin = user.Pin,
                        Archived = user.Archived,
                        Trash = user.Trash,
                        Reminder = user.Reminder
                    }).
                    ToList();

                if (userNoteLists == null)
                {
                    return null;
                }
                return userNoteLists;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        

        public List<UserNoteResponseData> GetPinnedNotes(int userID)
        {
            try
            {
                List<UserNoteResponseData> userNoteLists = _context.UserNotes.
                    Where(user => user.UserId == userID && user.Pin == true).
                    Select(user => new UserNoteResponseData
                    {
                        NoteId = user.NotesId,
                        Title = user.Title,
                        Notes = user.Notes,
                        Color = user.Color,
                        Image = user.Image,
                        Pin = user.Pin,
                        Archived = user.Archived,
                        Trash = user.Trash,
                        Reminder = user.Reminder
                    }).
                    ToList();

                if (userNoteLists == null)
                {
                    return null;
                }
                return userNoteLists;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool TrashNote(int userID, int noteID)
        {
            try
            {
                bool responseData;
                var userData = _context.UserNotes.
                    Where(user => user.UserId == userID && user.NotesId == noteID).
                    First<UserNotesInfo>();
                userData.Trash = true;
                userData.Pin = false;
                userData.Archived = false;
                _context.SaveChanges();

                responseData = true;
                return responseData;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public bool ArchievedUnarchievedNote(int userID, int noteID)
        {
            try
            {
                bool responseData;
                var userData = _context.UserNotes.
                    Where(user => user.UserId == userID && user.NotesId == noteID).
                    First<UserNotesInfo>();
                if (userData.Archived == false)
                    userData.Archived = true;
                else
                    userData.Archived = false;
                _context.SaveChanges();

                responseData = true;
                return responseData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool PinUnpinNote(int userID, int noteID)
        {
            try
            {
                bool responseData;
                var userData = _context.UserNotes.
                    Where(user => user.UserId == userID && user.NotesId == noteID).
                    First<UserNotesInfo>();
                if (userData.Pin == false)
                    userData.Pin = true;
                else
                    userData.Pin = false;
                _context.SaveChanges();

                responseData = true;
                return responseData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
