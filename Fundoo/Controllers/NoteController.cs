using BusinessLayer.Interface;
using CommonLayer.RequestModels;
using CommonLayer.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;
using System.Drawing;

namespace Fundoo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NoteController : ControllerBase
    {
        private readonly IUserNoteBusiness _userNoteBusiness;
        private readonly IConfiguration _config;

        public NoteController(IUserNoteBusiness userNoteBusiness, IConfiguration config)
        {
            _userNoteBusiness = userNoteBusiness;
            _config = config;    
        }

        /// <summary>
        /// It shows all the notes
        /// </summary>
        /// <returns>If Data Found, It return 200ok else return NotFound Response And If any exception occured return BadRequest</returns>
        [HttpGet]
        [Route("")]
        public IActionResult GetAllUserNotes()
        {
            try
            {
                string message;
                bool success = false;
                var idClaim = HttpContext.User.Claims.FirstOrDefault(id => id.Type.Equals("id", StringComparison.InvariantCultureIgnoreCase));
                int userId = Convert.ToInt32(idClaim.Value);

                List<UserNoteResponseData> userNoteResponseDataList = _userNoteBusiness.GetAllUserNotes(userId);

                if (userNoteResponseDataList != null)
                {
                    return Ok(userNoteResponseDataList.ToList());
                }
                else
                {
                    message = "Not found";
                    return Ok(new { success, message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// It Creates Note
        /// </summary>
        /// <param name="userNoteData">Note Data</param>
        /// <returns>If data found return 200Ok, else not found response or bad request</returns>
        [HttpPost]
        [Route("")]
        public IActionResult CreateNote(UserNoteRequest userNoteData)
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(id => id.Type.Equals("id", StringComparison.InvariantCultureIgnoreCase));
                int userId = Convert.ToInt32(idClaim.Value);
                UserNoteResponseData data = _userNoteBusiness.CreateNote(userId, userNoteData);
                bool success = false;
                string message;
                if (userNoteData == null)
                {
                    message = "Try again";
                    return Ok(new { success, message });
                }
                else
                {
                    success = true;
                    message = "Notes Created Successfully";
                    return Ok(new { success, message, data });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// It is used to Trash Note
        /// </summary>
        /// <param name="noteID">Note ID</param>
        /// <returns>If data found return 200ok, else NotFound response or BadRequest</returns>
        [HttpPost]
        [Route("{noteID}/Trash")]
        public IActionResult TrashNote(int noteID)
        {
            try
            {
                bool success = false, data;
                string message;
                var idClaim = HttpContext.User.Claims.FirstOrDefault(id => id.Type.Equals("id", StringComparison.InvariantCultureIgnoreCase));
                int userID = Convert.ToInt32(idClaim.Value);

                data = _userNoteBusiness.TrashNote(userID, noteID);

                if (data)
                {
                    success = true;
                    message = "Note Successfully Trashed";
                    return Ok(new { success, message, data });
                }
                else
                {
                    message = "Note Trash Unsuccessfull, Try again!";
                    return Ok(new { success, message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// It is used to Archieve Note
        /// </summary>
        /// <param name="noteID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{noteID}/Archieve")]
        public IActionResult ArchievedNote(int noteID)
        {
            try
            {
                bool success = false, data;
                string message;
                var idClaim = HttpContext.User.Claims.FirstOrDefault(id => id.Type.Equals("id", StringComparison.InvariantCultureIgnoreCase));
                int userID = Convert.ToInt32(idClaim.Value);

                data = _userNoteBusiness.ArchievedUnarchievedNote(userID, noteID);

                if (data)
                {
                    success = true;
                    message = "Note Successfully Archieved";
                    return Ok(new { success, message, data });
                }
                else
                {
                    message = "Note Archieved Unsuccessfull, Try again!";
                    return Ok(new { success, message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// It is used to Pin note
        /// </summary>
        /// <param name="noteID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{noteID}/Pin")]
        public IActionResult PinNote(int noteID)
        {
            try
            {
                bool success = false, data;
                string message;
                var idClaim = HttpContext.User.Claims.FirstOrDefault(id => id.Type.Equals("id", StringComparison.InvariantCultureIgnoreCase));
                int userID = Convert.ToInt32(idClaim.Value);

                data = _userNoteBusiness.PinUnpinNote(userID, noteID);

                if (data)
                {
                    success = true;
                    message = "Note Successfully Pinned";
                    return Ok(new { success, message, data });
                }
                else
                {
                    message = "Note pinned Unsuccessfull, Try again!";
                    return Ok(new { success, message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// It shows all Trashed notes
        /// </summary>
        /// <returns>If Data Found, It return 200ok else return NotFound Response And If any exception occured return BadRequest</returns>
        [HttpGet]
        [Route("Trash")]
        public IActionResult GetAllTrashedNotes()
        {
            try
            {
                bool success = false;
                string message;
                var idClaim = HttpContext.User.Claims.FirstOrDefault(id => id.Type.Equals("id", StringComparison.InvariantCultureIgnoreCase));
                int userId = Convert.ToInt32(idClaim.Value);

                List<UserNoteResponseData> userNoteResponseDataList = _userNoteBusiness.GetTrashedNotes(userId);

                if (userNoteResponseDataList != null)
                {
                    return Ok(userNoteResponseDataList.ToList());
                }
                else
                {
                    message = "No Trashed Notes";
                    return Ok(new { success, message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// It shows all Archieve notes
        /// </summary>
        /// <returns>If Data Found, It return 200ok else return NotFound Response And If any exception occured return BadRequest</returns>
        [HttpGet]
        [Route("Archieve")]
        public IActionResult GetAllArchievedNotes()
        {
            try
            {
                string message;
                bool success = false;
                var idClaim = HttpContext.User.Claims.FirstOrDefault(id => id.Type.Equals("id", StringComparison.InvariantCultureIgnoreCase));
                int userId = Convert.ToInt32(idClaim.Value);

                List<UserNoteResponseData> userNoteResponseDataList = _userNoteBusiness.GetArchievedNotes(userId);

                if (userNoteResponseDataList != null)
                {
                    return Ok(userNoteResponseDataList.ToList());
                }
                else
                {
                    message = "Not found";
                    return Ok(new { success, message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// It shows Pinned notes
        /// </summary>
        /// <returns>If Data Found, It return 200ok else return NotFound Response And If any exception occured return BadRequest</returns>
        [HttpGet]
        [Route("Pin")]
        public IActionResult GetPinnedNotes()
        {
            try
            {
                bool success = false;
                string message;
                var idClaim = HttpContext.User.Claims.FirstOrDefault(id => id.Type.Equals("id", StringComparison.InvariantCultureIgnoreCase));
                int userId = Convert.ToInt32(idClaim.Value);

                List<UserNoteResponseData> userNoteResponseDataList = _userNoteBusiness.GetPinnedNotes(userId);

                if (userNoteResponseDataList != null)
                {
                    return Ok(userNoteResponseDataList.ToList());
                }
                else
                {
                    message = "Not found";
                    return Ok(new { success, message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// It is used to show all Reminder Notes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Reminder")]
        public IActionResult GetReminders()
        {
            try
            {
                bool success = false;
                string message;
                var idClaim = HttpContext.User.Claims.FirstOrDefault(id => id.Type.Equals("id", StringComparison.InvariantCultureIgnoreCase));
                int userId = Convert.ToInt32(idClaim.Value);

                List<UserNoteResponseData> userNoteResponseDataList = _userNoteBusiness.GetReminders(userId);

                if (userNoteResponseDataList != null)
                {
                    return Ok(userNoteResponseDataList.ToList());
                }
                else
                {
                    message = "Not found";
                    return Ok(new { success, message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }


        /// <summary>
        /// It Deletes Note Forever
        /// </summary>
        /// <param name="noteID">Note ID</param>
        /// <returns>If Data Found, It return 200ok else return NotFound Response And If any exception occured return BadRequest</returns>
        [HttpDelete]
        [Route("{noteID}")]
        public IActionResult DeleteNote(int noteID)
        {
            try
            {
                bool data = _userNoteBusiness.DeleteNote(noteID);
                bool success = false;
                string message;
                if (data)
                {
                    success = true;
                    message = "Note Deleted Successfully";
                    return Ok(new { success, message });
                }
                else
                {
                    message = "Try again";
                    return Ok(new { success, message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// It Update Note
        /// </summary>
        /// <param name="noteID">Note ID</param>
        /// <param name="updateNoteRequest">Update Note Data</param>
        /// <returns>If Data Found, It return 200ok else return NotFound Response And If any exception occured return BadRequest</returns>
        [HttpPut]
        [Route("{noteID}")]
        public IActionResult UpdateNotes(int noteID, UpdateNoteRequest updateNoteRequest)
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(id => id.Type.Equals("id", StringComparison.InvariantCultureIgnoreCase));
                int userId = Convert.ToInt32(idClaim.Value);
                UserNoteResponseData userUpdateData = _userNoteBusiness.UpdateNote(userId, noteID, updateNoteRequest);
                bool success = false;
                string message;
                if (userUpdateData == null)
                {
                    message = "Try again";
                    return Ok(new { success, message });
                }
                else
                {
                    success = true;
                    message = "Notes Updated Successfully";
                    return Ok(new { success, message, userUpdateData });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });

            }
        }

        /// <summary>
        /// It is used to set Reminder
        /// </summary>
        /// <param name="noteID">Note ID</param>
        /// <param name="reminder">Reminder</param>
        /// <returns>If Data Found, It return 200ok else return NotFound Response And If any exception occured return BadRequest</returns>
        [HttpPut]
        [Route("{noteID}/Reminder")]
        public IActionResult AddReminder(int noteID, ReminderRequest reminder)
        {
            try
            {
                bool success = false, data;
                string message;
                var idClaim = HttpContext.User.Claims.FirstOrDefault(id => id.Type.Equals("id", StringComparison.InvariantCultureIgnoreCase));
                int userId = Convert.ToInt32(idClaim.Value);
                data = _userNoteBusiness.AddReminder(userId, noteID, reminder);
                
                if (data)
                {
                    success = true;
                    message = "Reminder Set Successfully";
                    return Ok(new { success, message });
                }
                else
                {
                    message = "Try Again!";
                    return Ok(new { success, message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });

            }
        }

        /// <summary>
        /// It is used to Add Color
        /// </summary>
        /// <param name="noteID">NoteID</param>
        /// <param name="color">Color</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{noteID}/Color")]
        public IActionResult AddColor(int noteID, ColorRequest color)
        {
            try
            {
                bool success = false, data;
                string message;
                var idClaim = HttpContext.User.Claims.FirstOrDefault(id => id.Type.Equals("id", StringComparison.InvariantCultureIgnoreCase));
                int userId = Convert.ToInt32(idClaim.Value);
                data = _userNoteBusiness.AddColor(userId, noteID, color);
                
                if (data)
                {
                    success = true;
                    message = "Color Set Successfully";
                    return Ok(new { success, message });
                }
                else
                {
                    message = "Try Again!";
                    return Ok(new { success, message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });

            }
        }

        /// <summary>
        /// It is used to Add Image
        /// </summary>
        /// <param name="noteID">NoteID</param>
        /// <param name="imageFile">Select Image</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{noteID}/Image")]
        public IActionResult AddImage(int noteID, IFormFile imageFile)
        {
            try
            {
                bool success = false;
                string message;
                var idClaim = HttpContext.User.Claims.FirstOrDefault(id => id.Type.Equals("id", StringComparison.InvariantCultureIgnoreCase));
                int userID = Convert.ToInt32(idClaim.Value);
                string image = UploadImageToCloud(imageFile);
                ImageRequest imageRequest = new ImageRequest
                {
                    Image = image
                };
                bool data = _userNoteBusiness.AddImage(userID, noteID, imageRequest);
                if (data)
                {
                    success = true;
                    message = "Image Added Successfully";
                    return Ok(new { success, message });
                }
                else
                {
                    message = "Unsuccessfull";
                    return Ok(new { success, message });
                }
            }
            catch(Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// It is used to Add Label to Note
        /// </summary>
        /// <param name="noteID"></param>
        /// <param name="addLabelNote"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{noteID}/Label")]
        public IActionResult AddLabelToANote(int noteID, AddLabelNoteRequest addLabelNote)
        {
            try
            {
                bool success = false;
                string message;
                var idClaim = HttpContext.User.Claims.FirstOrDefault(id => id.Type.Equals("id", StringComparison.InvariantCultureIgnoreCase));
                int userID = Convert.ToInt32(idClaim.Value);
                UserNoteResponseData data = _userNoteBusiness.AddlabelsToNote(userID, noteID, addLabelNote);
                if (data != null)
                {
                    success = true;
                    message = "Label Added to note Succesfully";
                    return Ok(new { success, message, data });
                }
                else
                {
                    message = "Try Again!";
                    return Ok(new { success, message, data });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// It is used to Add Collaborator to a Note
        /// </summary>
        /// <param name="noteID">NoteID</param>
        /// <param name="collaborators">Collaborator ID</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{noteID}/Collaborator")]
        public IActionResult AddCollaborator(int noteID, CollaboratorsRequest collaborators)
        {
            try
            {
                bool success = false;
                string message;
                var idClaim = HttpContext.User.Claims.FirstOrDefault(id => id.Type.Equals("id", StringComparison.InvariantCultureIgnoreCase));
                int userID = Convert.ToInt32(idClaim.Value);
                UserNoteResponseData data = _userNoteBusiness.AddCollaborator(userID, noteID, collaborators);
                if (data != null)
                {
                    success = true;
                    message = "Collaborator Added Successfully";
                    return Ok(new { success, message, data });
                }
                else
                {
                    message = "Try Again!";
                    return Ok(new { success, message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// It is used to remove collaborator
        /// </summary>
        /// <param name="noteID">NoteID</param>
        /// <param name="collaborator">Collaborator ID</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{noteID}/RemoveCollaborator")]
        public IActionResult RemoveCollaborator(int noteID, CollaboratorRequest collaborator)
        {
            try
            {
                bool success = false;
                string message;
                var idClaim = HttpContext.User.Claims.FirstOrDefault(id => id.Type.Equals("id", StringComparison.InvariantCultureIgnoreCase));
                int userID = Convert.ToInt32(idClaim.Value);
                bool data = _userNoteBusiness.RemoveCollaborator(userID, noteID, collaborator);
                if (data)
                {
                    success = true;
                    message = "Collaborator Removed Successfully";
                    return Ok(new { success, message });
                }
                else
                {
                    message = "Enter Proper Collaborator ID";
                    return NotFound(new { success, message });
                }
            }
            catch(Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        private string UploadImageToCloud(IFormFile image)
        {
            try
            {
                var myAccount = new Account(_config["Cloudinary:CloudName"], _config["Cloudinary:ApiKey"], _config["Cloudinary:ApiSecret"]);

                Cloudinary _cloudinary = new Cloudinary(myAccount);

                var imageUpload = new ImageUploadParams
                {
                    File = new FileDescription(image.FileName, image.OpenReadStream()),
                };

                var uploadResult = _cloudinary.Upload(imageUpload);

                return uploadResult.SecureUri.AbsoluteUri;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}